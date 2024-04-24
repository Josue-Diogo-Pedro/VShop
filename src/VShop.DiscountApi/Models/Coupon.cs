using System.ComponentModel.DataAnnotations;

namespace VShop.DiscountApi.Models;

public class Coupon
{
    [Required]
    [StringLength(30)]
    public int CouponId { get; set; }
    
    public string? CouponCode { get; set; }

    [Required]
    public decimal Discount { get; set; }
}
