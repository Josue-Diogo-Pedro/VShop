using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories;

public class ProductRepository : IProductRepository
{

    public Task<Product> Create(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<Product> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> Update(Product product)
    {
        throw new NotImplementedException();
    }
}
