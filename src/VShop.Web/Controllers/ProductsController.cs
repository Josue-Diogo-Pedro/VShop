using Microsoft.AspNetCore.Mvc;

namespace VShop.Web.Controllers;

public class ProductsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
