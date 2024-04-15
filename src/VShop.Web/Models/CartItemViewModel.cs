namespace VShop.Web.Models;

public class CartItemViewModel
{
    public int CartItemId { get; set; }
    public int Quantity { get; set; } = 1;
    public int ProductId { get; set; }
    public int CartHeaderId { get; set; }
    public ProductViewModel Product { get; set; } = new();
}
