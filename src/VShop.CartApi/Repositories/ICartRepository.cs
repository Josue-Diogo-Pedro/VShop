using VShop.CartApi.DTOs;

namespace VShop.CartApi.Repositories;

public interface ICartRepository
{
    Task<CartDTO> GetCartByUserIdAsync(string userId);
    Task<CartDTO> UpdateCartAsync(CartDTO cartDTO);
    Task<bool> CleanCartAsync(string userId);
    Task<bool> DeleteCartItemAsync(int cartItemId);

    Task<bool> ApplyCouponAsync(string userId, string couponCode);
}
