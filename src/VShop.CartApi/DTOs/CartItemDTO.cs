using VShop.CartApi.Models;

namespace VShop.CartApi.DTOs;

public class CartItemDTO
{
    public int CartItemId { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int CartHeaderId { get; set; }
    public Product Product { get; set; } = new();
    public CartHeader CartHeader { get; set; } = new();
}
