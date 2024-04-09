using VShop.CartApi.Context;
using VShop.CartApi.DTOs;

namespace VShop.CartApi.Repositories;

public class CartRepository : ICartRepository
{
    private readonly CartApiDbContext _context;

    public CartRepository(CartApiDbContext context)
    {
        _context = context;
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

    public Task<CartDTO> GetCartByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<CartDTO> UpdateCartAsync(CartDTO cartDTO)
    {
        throw new NotImplementedException();
    }
}
