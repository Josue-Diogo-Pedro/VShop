using VShop.CartApi.Models;

namespace VShop.CartApi.DTOs;

public class CartItemDTO
{
    public int CartItemId { get; set; }
    public int Quantity { get; set; } = 1;
    public int ProductId { get; set; }
    public int CartHeaderId { get; set; }
    public ProductDTO Product { get; set; } = new();
    //public CartHeaderDTO CartHeader { get; set; } = new();
}
