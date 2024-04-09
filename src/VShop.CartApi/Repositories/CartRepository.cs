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

    public Task<bool> ApplyCouponAsync(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CleanCartAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCartItemAsync(int cartItemId)
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
}
