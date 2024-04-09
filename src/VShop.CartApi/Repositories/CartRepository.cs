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

    public Task<bool> ApplyCouponAsync(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }



    public Task<bool> DeleteCouponAsync(string userId)
    {
        throw new NotImplementedException();
    }


    public Task<CartDTO> UpdateCartAsync(CartDTO cartDTO)
    {
        throw new NotImplementedException();
    }

    private async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}
