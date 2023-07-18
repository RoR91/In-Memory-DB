using AutoMapper;
using Challenge.Data.Entities;
using Challenge.Models;
using Challenge.Models.DTOs;

namespace Challenge.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, CategoryRequest>();

            CreateMap<AddCategoryRequest, Category>();
            CreateMap<Category, AddCategoryRequest>();

            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();

            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductRequest>();

            CreateMap<AddProductRequest, Product>();
            CreateMap<Product, AddProductRequest>();

            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();
        }
    }
}
