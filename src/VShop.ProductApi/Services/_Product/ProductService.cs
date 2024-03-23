using AutoMapper;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories;

namespace VShop.ProductApi.Services._Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var products = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDTO>>(products);
    }

    public async Task<ProductDTO> GetProductById(int id)
    {
        var product = await _productRepository.GetById(id);
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task AddProduct(ProductDTO product)
    {
        var productResult = _mapper.Map<Product>(product);
        await _productRepository.Create(productResult);
        product.ProductId = productResult.ProductId;
    }

    public async Task UpdateProduct(ProductDTO product)
    {
        var productResult = _mapper.Map<Product>(product);
        await _productRepository.Update(productResult);
    }

    public async Task RemoveProduct(int id)
    {
        var product = await _productRepository.GetById(id);
        await _productRepository.Delete(product.ProductId);
    }

}
