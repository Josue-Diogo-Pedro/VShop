namespace VShop.Web.Models;

public class CartHeaderViewModel
{
    public int CartHeaderId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CouponCode { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; } = 0.00m;

    //Receive discount
    public decimal Discount { get; set; } = 0.00m;

    //checkout
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public string Telephone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    //Credit card
    public string CardNumber { get; set; } = string.Empty;
    public string NameOnCard { get; set; } = string.Empty;
    public string CVV { get; set; } = string.Empty;
    public string ExpireMonthYear { get; set; } = string.Empty;
}
