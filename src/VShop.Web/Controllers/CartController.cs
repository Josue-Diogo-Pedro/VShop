using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService) => _cartService = cartService;

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    #region Private Functions

    private async Task<string> GetAccessToken() => await HttpContext.GetTokenAsync("access_token");

    private string GetUserId() => User.Claims?.Where(user => user.Type == "sub")?.FirstOrDefault()?.Value;

    private async Task<CartViewModel> GetCartByUserId()
    {
        var cart = await _cartService.GetCartByUserIdAsync(GetUserId(), await GetAccessToken());

        if(cart?.CartHeader is null)
        {
            foreach (var item in cart?.CartItems)
            {
                cart.CartHeader.TotalAmount += (item.Product.Price * item.Quantity);
            }
        }

        return cart;
    }

    #endregion
}
