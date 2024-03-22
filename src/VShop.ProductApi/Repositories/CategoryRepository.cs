using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public Task<Category> Create(Category category)
    {
        throw new NotImplementedException();
    }

    public Task<Category> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetCategoriesProducts()
    {
        throw new NotImplementedException();
    }

    public Task<Category> Update(Category category)
    {
        throw new NotImplementedException();
    }
}
