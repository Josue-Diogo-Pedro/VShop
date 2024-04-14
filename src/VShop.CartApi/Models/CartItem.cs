namespace VShop.CartApi.Models;

public class CartItem
{
    public int CartItemId { get; set; }
    public int Quantity { get; set; } = 1;
    public int ProductId { get; set; }
    public int CartHeaderId { get; set; }
    public Product Product { get; set; } = new();
    //public CartHeader CartHeader { get; set; } = new();
}
