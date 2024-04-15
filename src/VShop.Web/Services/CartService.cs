using System.Net.Http.Headers;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services;

public class CartService : ICartService
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

    public async Task<CartViewModel> GetCartByUserIdAsync(string userId, string token)
    {
        var client = _httpClientFactory.CreateClient("CartApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.GetAsync($"{apiEndpoint}/getcart/{userId}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var responseApi = await response.Content.ReadAsStreamAsync();
                cartViewModel = await JsonSerializer.DeserializeAsync<CartViewModel>(responseApi, _optins);
            }
            else return null;
        }
        return cartViewModel;
    }

    public Task<bool> RemoveItemFromCartAsync(int cartId, string token)
    {
        throw new NotImplementedException();
    }

    public Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM, string token)
    {
        throw new NotImplementedException();
    }
    public Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM, string token)
    {
        throw new NotImplementedException();
    }
    public Task<bool> ClearCartAsync(string userId, string token)
    {
        throw new NotImplementedException();
    }

    #region Private functions

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
        => client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    #endregion

    #region Report and Checkout

    public Task<bool> ApplyCouponAsync(CartViewModel cartVM, string couponCode, string token)
    {
        throw new NotImplementedException();
    }
    public Task<CartViewModel> Chekout(CartHeaderViewModel cartHeader, string token)
    {
        throw new NotImplementedException();
    }
    public Task<bool> RemoveCouponAsync(string userId, string token)
    {
        throw new NotImplementedException();
    }

    #endregion

}
