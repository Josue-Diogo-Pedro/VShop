using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.CartApi.DTOs;
using VShop.CartApi.Repositories;

namespace VShop.CartApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;

	public CartController(ICartRepository cartRepository) => _cartRepository = cartRepository;

    [HttpGet("getcart/{userId}")]
    public async Task<ActionResult<CartDTO>> GetCartByUserId(string userId)
    {
        var cartDto = await _cartRepository.GetCartByUserIdAsync(userId);

        if (cartDto is null) return NotFound();

        return Ok(cartDto);
    }

    [HttpPost("addcart")]
    public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
    {
        var cart = await _cartRepository.UpdateCartAsync(cartDTO);

        if (cart is null) return BadRequest();

        return Ok(cart);
    }

    [HttpPut("updatecart")]
    public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDTO)
    {
        var cart = await _cartRepository.UpdateCartAsync(cartDTO);

        if(cart is null) return NotFound();

        return Ok(cart);
    }

    [HttpDelete("deletecart/{id}")]
    public async Task<ActionResult<bool>> DeleteCart(int id)
    {
        var status = await _cartRepository.DeleteCartItemAsync(id);

        if (!status) return BadRequest();

        return Ok(status);
    }

    [HttpPost("applycoupon")]
    public async Task<ActionResult<bool>> ApplyCoupon(CartDTO cartDTO)
    {
        var result = await _cartRepository.ApplyCouponAsync(cartDTO.CartHeader.UserId, cartDTO.CartHeader.CouponCode);

        if (!result) return NotFound($"CartHeader not found for userId = {cartDTO.CartHeader.UserId}");

        return Ok(result);
    }

    [HttpDelete("deletecoupon/{userId}")]
    public async Task<ActionResult<bool>> DeleteCoupon(string userId)
    {
        var result = await _cartRepository.DeleteCouponAsync(userId);

        if (!result) return NotFound($"Discount coupon not found for userId = {userId}");

        return Ok(result);
    }
}
