namespace VShop.Web.Models;

public class CartViewModel
{
    public CartHeaderViewModel CartHeader { get; set; } = new();
    public IEnumerable<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
}
