using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services._Category;
using VShop.Web.Roles;

namespace VShop.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService) => _categoryService = categoryService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        var categories = await _categoryService.GetCategories();

        if (categories is null) return NotFound("Categories not found");

        return Ok(categories);
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProduct()
    {
        var categoriesProducts = await _categoryService.GetCategoriesProducts();

        if (categoriesProducts is null) return NotFound("Categories not found");

        return Ok(categoriesProducts);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        var category = await _categoryService.GetCategoryById(id);

        if (category is null) return NotFound("Category not found");

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody]CategoryDTO categoryDTO)
    {
        if (categoryDTO is null) return BadRequest("Invalid Data");

        await _categoryService.AddCategory(categoryDTO);

        return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.CategoryId }, categoryDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody]CategoryDTO categoryDTO)
    {
        if (id != categoryDTO.CategoryId) return BadRequest();

        if (categoryDTO is null) return BadRequest();

        await _categoryService.UpdateCategory(categoryDTO);

        return Ok(categoryDTO);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult> Delete(int id)
    {
        var category = await _categoryService.GetCategoryById(id);

        if (category is null) return NotFound("Category not found");

        await _categoryService.RemoveCategory(category.CategoryId);

        return Ok(category);
    }
}
