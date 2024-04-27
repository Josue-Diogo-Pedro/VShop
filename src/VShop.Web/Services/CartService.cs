using System.Net.Http.Headers;
using System.Text;
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

    public async Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM, string token)
    {
        var client = _httpClientFactory.CreateClient("CartApi");
        PutTokenInHeaderAuthorization(token, client);

        StringContent content = new(JsonSerializer.Serialize(cartVM), Encoding.UTF8, "application/json");

        using(var response = await client.PostAsync($"{apiEndpoint}/addcart/", content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                cartViewModel = await JsonSerializer.DeserializeAsync<CartViewModel>(apiResponse, _optins);
            }
            else return null;
        }
        return cartViewModel;
    }

    public async Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM, string token)
    {
        var client = _httpClientFactory.CreateClient("CartApi");
        PutTokenInHeaderAuthorization(token, client);

        CartViewModel cartUpdated = new();

        using(var response = await client.PutAsJsonAsync($"{apiEndpoint}/updatecart/", cartVM))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                cartUpdated = await JsonSerializer.DeserializeAsync<CartViewModel>(apiResponse, _optins);
            }
            else return null;
        }

        return cartUpdated;
    }

    public async Task<bool> RemoveItemFromCartAsync(int cartId, string token)
    {
        var client = _httpClientFactory.CreateClient("CartApi");
        PutTokenInHeaderAuthorization(token, client);

        using(var response = await client.DeleteAsync($"{apiEndpoint}/deletecart/" + cartId))
        {
            if (response.IsSuccessStatusCode) return true;
        }
        return false;
    }

    #region Private functions

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
        => client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    #endregion

    #region Report and Checkout

    public Task<bool> ClearCartAsync(string userId, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ApplyCouponAsync(CartViewModel cartVM, string token)
    {
        var client = _httpClientFactory.CreateClient("CartApi");
        PutTokenInHeaderAuthorization(token, client);

        StringContent content = new(JsonSerializer.Serialize(cartVM), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync($"{apiEndpoint}/applycoupon/", content))
        {
            if(response.IsSuccessStatusCode) return true;
        }

        return false;

    }
    public async Task<CartHeaderViewModel> Chekout(CartHeaderViewModel cartHeader, string token)
    {
        CartHeaderViewModel? cartheaderVM = new();
        var client = _httpClientFactory.CreateClient("CartApi");
        PutTokenInHeaderAuthorization(token, client);

        StringContent content = new(JsonSerializer.Serialize(cartHeader), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync($"{apiEndpoint}/checkout/", content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                cartheaderVM = await JsonSerializer.DeserializeAsync<CartHeaderViewModel>(apiResponse, _optins);
            }
            else return null;
        }
        return cartheaderVM;
    }
    public async Task<bool> RemoveCouponAsync(string userId, string token)
    {
        var client = _httpClientFactory.CreateClient("CartApi");
        PutTokenInHeaderAuthorization(token, client);

        using(var response = await client.DeleteAsync($"{apiEndpoint}/deletecoupon/{userId}"))
        {
            if (response.IsSuccessStatusCode) return true;
        }

        return false;
    }

    #endregion

}
