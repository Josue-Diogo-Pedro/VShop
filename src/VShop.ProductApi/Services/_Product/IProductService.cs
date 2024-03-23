using VShop.ProductApi.DTOs;

namespace VShop.ProductApi.Services._Product;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetProducts();
    Task<ProductDTO> GetProductById(int id);
    Task AddProduct(ProductDTO product);
    Task UpdateProduct(ProductDTO product);
    Task RemoveProduct(int id);
}
