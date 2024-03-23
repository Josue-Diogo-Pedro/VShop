using VShop.ProductApi.DTOs;

namespace VShop.ProductApi.Services._Category;

public class CategoryService : ICategoryService
{


    public Task<IEnumerable<CategoryDTO>> GetCategories()
    {
        throw new NotImplementedException();
    }
    public Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
    {
        throw new NotImplementedException();
    }
    public Task<CategoryDTO> GetCategoryById(int id)
    {
        throw new NotImplementedException();
    }
    public Task AddCategory(CategoryDTO categoryDTO)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCategory(CategoryDTO categoryDTO)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCategory(int id)
    {
        throw new NotImplementedException();
    }

}
