using VShopping.DiscountAPI.DTOs;

namespace VShopping.DiscountAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCouponByCode (string couponCode);

    }
}
