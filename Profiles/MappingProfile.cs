using AutoMapper;
using CategoriesProductsAPI.Dtos;
using CategoriesProductsAPI.Models;

namespace CategoriesProductsAPI.Profiles{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, CategoryProductCountDto>()
                .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products.Count));

            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore());
        }
    }
}