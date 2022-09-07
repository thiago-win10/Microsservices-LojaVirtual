using AutoMapper;
using VShopping.DiscountAPI.Model;

namespace VShopping.DiscountAPI.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CouponDTO, Coupon>().ReverseMap();
        }
    }
}
