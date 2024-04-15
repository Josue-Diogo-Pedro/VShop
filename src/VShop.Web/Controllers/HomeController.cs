using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProducts(string.Empty);
        if (products is null) return View("Error");

        return View(products);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ProductViewModel>> ProductDetails(int id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");

        var product = await _productService.FindProductById(id, token);
        if (product is null) return View("Error");

        return View(product);
    }

    [HttpPost]
    [ActionName("ProductDetails")]
    [Authorize]
    public async Task<ActionResult<ProductViewModel>> ProductDetailPost(ProductViewModel productViewModel)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        CartViewModel cart = new()
        {
            CartHeader = new()
            {
                UserId = User.Claims.Where(c => c.Type == "sub")?.FirstOrDefault()?.Value
            }
        };

        CartItemViewModel cartItem = new()
        {
            Quantity = productViewModel.Quantity,
            ProductId = productViewModel.ProductId,
            Product = await _productService.FindProductById(productViewModel.ProductId, token)
        };

        List<CartItemViewModel> cartItems = new();
        cartItems.Add(cartItem);

        cart.CartItems = cartItems;

        var result = await _cartService.AddItemToCartAsync(cart, token);

        if (result is not null) return RedirectToAction(nameof(Index));

        return View(productViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public async Task<IActionResult> Login()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Logout()
    {
        return SignOut("Coockies", "oidc");
    }
}