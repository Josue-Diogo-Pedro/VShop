using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VShop.CartApi.Context;
using VShop.CartApi.DTOs;
using VShop.CartApi.Models;

namespace VShop.CartApi.Repositories;

public class CartRepository : ICartRepository
{
    private readonly CartApiDbContext _context;
    private readonly IMapper _mapper;

    public CartRepository(CartApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<CartDTO> GetCartByUserIdAsync(string userId)
    {
        Cart cart = new()
        {
            CartHeader = await _context.CartHeader
                                                ?.AsNoTracking()
                                                ?.DefaultIfEmpty()
                                                ?.SingleOrDefaultAsync(header => header.UserId == userId)
        };

        cart.CartItems = await _context.CartItems?
                                                .AsNoTracking()?
                                                .DefaultIfEmpty()?
                                                .Where(item => item.CartHeaderId == cart.CartHeader.CartHeaderId)
                                                .Include(prod => prod.Product).ToListAsync();

        return _mapper.Map<CartDTO>(cart);

    }
    public async Task<bool> DeleteCartItemAsync(int cartItemId)
    {
        try
        {
            CartItem cartItem = await _context.CartItems?
                                                        .AsNoTracking()?
                                                        .DefaultIfEmpty()?
                                                        .SingleOrDefaultAsync(c => c.CartItemId == cartItemId);

            int total = await _context.CartItems?
                                                .AsNoTracking()?
                                                .DefaultIfEmpty()?
                                                .Where(c => c.CartHeaderId == cartItem.CartHeaderId)?
                                                .CountAsync();

            if(total == 1)
            {
                CartHeader cartHeaderRemove = await _context.CartHeader?
                                                                        .AsNoTracking()?
                                                                        .DefaultIfEmpty()?
                                                                        .SingleOrDefaultAsync(c => c.CartHeaderId == cartItem.CartHeaderId);

                _context.CartHeader.Remove(cartHeaderRemove);
            }

            _context.CartItems.Remove(cartItem);
            await SaveChangesAsync();

            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> CleanCartAsync(string userId)
    {
        CartHeader cartHeader = await _context.CartHeader?
                                                        .AsNoTracking()?
                                                        .DefaultIfEmpty()?
                                                        .SingleOrDefaultAsync(c => c.UserId == userId);

        if(cartHeader is not null)
        {
            _context.CartItems.RemoveRange(await _context.CartItems?
                                                                    .AsNoTracking()
                                                                    .DefaultIfEmpty()?
                                                                    .Where(c => c.CartHeaderId == cartHeader.CartHeaderId)?
                                                                    .ToListAsync()
                                          );

            _context.CartHeader.Remove(cartHeader);

            await SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<CartDTO> UpdateCartAsync(CartDTO cartDTO)
    {
        Cart cart = _mapper.Map<Cart>(cartDTO);

        //save product in database if isn't exist
        await SaveProductInDataBase(cart, cartDTO);

        //verify if CartHeader is null
        CartHeader cartHeader = await _context.CartHeader?
                                                        .AsNoTracking()?
                                                        .DefaultIfEmpty()?
                                                        .SingleOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);
        if(cartHeader is null)
        {
            //create CartHeader and items
            await CreateCartHeaderAndItems(cart);
        }
        else
        {
            //update quantity and items
            await UpdateQuantityAndItems(cart, cartDTO, cartHeader);
        }

        return _mapper.Map<CartDTO>(cart);
    }

    public Task<bool> ApplyCouponAsync(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCouponAsync(string userId)
    {
        throw new NotImplementedException();
    }

    #region Private Functions
    
    private async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    private async Task SaveProductInDataBase(Cart cart, CartDTO cartDTO)
    {
        //verify if product just was save, if not save
        Product product = await _context.Products?
                                                .AsNoTracking()?
                                                .DefaultIfEmpty()?
                                                .SingleOrDefaultAsync(p => p.ProductId == cartDTO.CartItems.FirstOrDefault().ProductId);

        if(product is null)
        {
            await _context.Products.AddAsync(cart.CartItems?.FirstOrDefault()?.Product);
            await SaveChangesAsync();
        }
    }

    private async Task CreateCartHeaderAndItems(Cart cart)
    {
        //Create CartHeader and CartItems
        await _context.CartHeader.AddAsync(cart.CartHeader);
        await SaveChangesAsync();

        cart.CartItems.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
        cart.CartItems.FirstOrDefault().Product = null;

        await _context.CartItems.AddAsync(cart.CartItems.FirstOrDefault());
        await SaveChangesAsync();
    }

    private async Task UpdateQuantityAndItems(Cart cart, CartDTO cartDTO, CartHeader? cartHeader)
    {
        //If CartHeader is not null
        //Verify if contains the same product
        var cartDetails = await _context.CartItems?
                                                  .AsNoTracking()?
                                                  .DefaultIfEmpty()?
                                                  .SingleOrDefaultAsync(p => p.ProductId == cartDTO.CartItems.FirstOrDefault().ProductId && p.CartHeaderId == cartHeader.CartHeaderId);

        if(cartDetails is null)
        {
            //Create the CartItems
            cart.CartItems.FirstOrDefault().CartHeaderId = cartHeader.CartHeaderId;
            cart.CartItems.FirstOrDefault().Product = null;
            await _context.CartItems.AddAsync(cart.CartItems.FirstOrDefault());
            await SaveChangesAsync();
        }
        else
        {
            //Update quantity and CartItems
            cart.CartItems.FirstOrDefault().Product = null;
            cart.CartItems.FirstOrDefault().Quantity += cartDetails.Quantity;
            cart.CartItems.FirstOrDefault().CartItemId = cartDetails.CartItemId;
            cart.CartItems.FirstOrDefault().CartHeaderId = cartDetails.CartHeaderId;
            _context.CartItems.Update(cart.CartItems.FirstOrDefault());
            await SaveChangesAsync();
        }
    }

    #endregion

}
