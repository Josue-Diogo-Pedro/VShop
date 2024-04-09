using VShop.CartApi.Models;

namespace VShop.CartApi.DTOs;

public class CartDTO
{
    public CartHeader CartHeader { get; set; } = new();
    public IEnumerable<CartItem> CartItems { get; set; } = new List<CartItem>();
}
