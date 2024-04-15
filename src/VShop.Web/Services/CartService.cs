using System.Text.Json;
using VShop.Web.Models;

namespace VShop.Web.Services;

public class CartService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions? _optins;
    private string apiEndpoint = "/api/cart";
    private CartViewModel cartViewModel = new();

    public CartService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _optins = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }


}
