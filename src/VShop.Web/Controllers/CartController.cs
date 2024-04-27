using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly ICouponService _couponService;

    public CartController(ICartService cartService, ICouponService couponService)
    {
        _cartService = cartService;
        _couponService = couponService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        CartViewModel? cartVM = await GetCartByUserId();

        if(cartVM is null)
        {
            ModelState.AddModelError("CartNotFound", "Does not exist a cart yet... Come on Shopping");
            return View("/Views/Cart/CartNotFound.cshtml");
        }

        return View(cartVM);
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CartViewModel cartViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _cartService.Chekout(cartViewModel.CartHeader, await GetAccessToken());
            if (result is not null) return RedirectToAction(nameof(CheckoutCompleted));
        }

        return View(cartViewModel);
    }

    public async Task<IActionResult> RemoveItem(int cartId)
    {
        var result = await _cartService.RemoveItemFromCartAsync(cartId, await GetAccessToken());

        if (result) return RedirectToAction(nameof(Index));

        return View(cartId);
    }

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        CartViewModel? cartVM = await GetCartByUserId();
        return View(cartVM);
    }

    [HttpPost]
    public async Task<IActionResult> ApplyCoupon(CartViewModel cartVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _cartService.ApplyCouponAsync(cartVM, await GetAccessToken());

            if (result) return RedirectToAction(nameof(Index));
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCoupon(string userId)
    {
        if (ModelState.IsValid)
        {
            var result = await _cartService.RemoveCouponAsync(userId, await GetAccessToken());

            if (result) return RedirectToAction(nameof(Index));
        }

            return View();
    }

    #region Private Functions

    private async Task<string> GetAccessToken() => await HttpContext.GetTokenAsync("access_token");

    private string GetUserId() => User.Claims?.Where(user => user.Type == "sub")?.FirstOrDefault()?.Value;

    private async Task<CartViewModel> GetCartByUserId()
    {
        var cart = await _cartService.GetCartByUserIdAsync(GetUserId(), await GetAccessToken());

        if(cart?.CartHeader is not null)
        {
            if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
            {
                var coupon = await _couponService.GetDiscountCoupon(cart.CartHeader.CouponCode, await GetAccessToken());

                if (coupon?.CouponCode is not null)
                    cart.CartHeader.Discount = coupon.Discount;
            }
        }

        foreach (var item in cart?.CartItems)
        {
            cart.CartHeader.TotalAmount += (item.Product.Price * item.Quantity);
        }

        cart.CartHeader.TotalAmount = cart.CartHeader.TotalAmount -  (cart.CartHeader.TotalAmount * cart.CartHeader.Discount) / 100;

        return cart;
    }

    #endregion
}
