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

    public Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> FindProductById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> CreateProduct(ProductViewModel productVM)
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> UpdateProduct(ProductViewModel productVM)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProductById(int id)
    {
        throw new NotImplementedException();
    }
}
