using Microsoft.AspNetCore.Mvc;

namespace VShop.Web.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
