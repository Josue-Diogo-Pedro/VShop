using VShop.CartApi.Models;

namespace VShop.CartApi.DTOs;

public class CartDTO
{
    public CartHeaderDTO CartHeader { get; set; } = new();
    public IEnumerable<CartItemDTO> CartItems { get; set; } = new List<CartItemDTO>();
}
