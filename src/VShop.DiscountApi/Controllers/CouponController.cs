using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.DiscountApi.DTOs;
using VShop.DiscountApi.Repositories;

namespace VShop.DiscountApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly ICouponRepository _couponRepository;

	public CouponController(ICouponRepository couponRepository) => _couponRepository = couponRepository ?? 
                                                                                        throw new ArgumentNullException(nameof(couponRepository));

    [HttpGet("{couponCode}")]
    [Authorize]
    public async Task<ActionResult<CouponDTO>> GetDiscountCouponByCode(string couponCode)
    {
        var coupon = await _couponRepository.GetCouponByCode(couponCode);

        if (coupon is null) return NotFound($"Coupon code: {couponCode} not found");

        return Ok(coupon);
    }
}
