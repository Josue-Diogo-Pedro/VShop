﻿using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/products/";
    private readonly JsonSerializerOptions _options;
    private IEnumerable<ProductViewModel> _productsVM;
    private ProductViewModel _productVM;

    public ProductService(IHttpClientFactory clientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts(string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHearerAuthotization(client, token);

        using(var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _productsVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
            }
            else return null;
        }
        return _productsVM;
    }

    public async Task<ProductViewModel> FindProductById(int id, string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHearerAuthotization(client, token);

        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _productVM = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
            else return null;
        }
        return _productVM;
    }

    public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM, string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHearerAuthotization(client, token);

        StringContent content = new(JsonSerializer.Serialize(productVM), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productVM = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
            else return null;
        }
        return productVM;
    }

    public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVM, string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHearerAuthotization(client, token);

        ProductViewModel productUpdated = new();

        using (var response = await client.PutAsJsonAsync(apiEndpoint+productVM.ProductId, productVM))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productUpdated = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
            else return null;
        }
        return productUpdated;
    }

    public async Task<bool> DeleteProductById(int id, string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        PutTokenInHearerAuthotization(client, token);

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode) return true;
        }
        return false;
    }

    private static void PutTokenInHearerAuthotization(HttpClient client, string token) =>
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
}
