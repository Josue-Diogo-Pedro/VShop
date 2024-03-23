using AutoMapper;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTOs.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Category, CategoryDTO>().ReverseMap();

		CreateMap<Product, ProductDTO>().ForMember(x => x.CategoryName, category => category.MapFrom(p => p.Category.Name));
        CreateMap<ProductDTO, Product>().ReverseMap();

    }
}
