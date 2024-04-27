using System.Net.Http.Headers;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services;

public class CouponService : ICouponService
{
    private readonly IHttpClientFactory _HttpClientFactory;
    private readonly JsonSerializerOptions _options;
    private const string _apiURL = "/api/coupon";
    private CouponViewModel couponVM = new();

    public CouponService(IHttpClientFactory httpClientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _HttpClientFactory = httpClientFactory;
    }

    public async Task<CouponViewModel> GetDiscountCoupon(string couponCode, string token)
    {
        var client = _HttpClientFactory.CreateClient("DiscountApi");
        PutTokenInHeaderAuthorization(client, token);

        using (var response = await client.GetAsync($"{_apiURL}/{couponCode}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiEndpoint = await response.Content.ReadAsStreamAsync();
                couponVM = await JsonSerializer.DeserializeAsync<CouponViewModel>(apiEndpoint, _options);
            }
            else return null;
        }

        return couponVM;
    }

    private void PutTokenInHeaderAuthorization(HttpClient client, string token) => 
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
}
