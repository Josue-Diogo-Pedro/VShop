using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services._Product;
using VShop.Web.Roles;

namespace VShop.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService) => _productService = productService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var products = await _productService.GetProducts();

        if (products is null) return NotFound("Products not found");

        return Ok(products);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var product = await _productService.GetProductById(id);

        if (product is null) return NotFound("Product not found");

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
    {
        if (productDTO is null) return BadRequest("Invalid data");

        await _productService.AddProduct(productDTO);

        return new CreatedAtRouteResult("GetProduct", new { id = productDTO.ProductId }, productDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
    {
        if (id != productDTO.ProductId) return BadRequest();

        if (productDTO is null) return BadRequest();

        await _productService.UpdateProduct(productDTO);

        return Ok(productDTO);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult> Delete(int id)
    {
        var product = await _productService.GetProductById(id);

        if (product is null) return NotFound("Product not found");

        await _productService.RemoveProduct(product.ProductId);

        return Ok(product);
    }

}
