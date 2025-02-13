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
            CreateMap<CategoryModel, Category>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<Product, ProductViewModel>().ReverseMap();

            CreateMap<ProductModel, Product>()
           .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
               src.ProductCategories!.Select(pc => pc.Category)));
            CreateMap<ProductModel, CatalogItemViewModel>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AvailableStock, opt => opt.MapFrom(src => src.Stock.Quantity));
        }
    }
}
