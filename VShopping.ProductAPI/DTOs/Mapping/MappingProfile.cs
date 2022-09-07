using AutoMapper;
using VShopping.ProductAPI.Models;

namespace VShopping.ProductAPI.DTOs.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<ProductDTO, Product>();

            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name));



        }
    }
}
