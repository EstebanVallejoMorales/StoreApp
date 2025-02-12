using AutoMapper;
using StoreApp.Entities;
using StoreApp.Models;
using StoreApp.Presenters.ViewModels;

namespace StoreApp.Mappers.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<CategoryModel, Category>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<ProductModel, Product>()
           .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
               src.ProductCategories!.Select(pc => pc.Category)));
        }
    }
}
