namespace VShop.CartApi.DTOs;

public class CartHeaderDTO
{
    public int CartHeaderId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CouponCode { get; set; } = string.Empty;
}
