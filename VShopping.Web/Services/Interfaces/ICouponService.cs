using VShopping.Web.Models;

namespace VShopping.Web.Services.Interfaces
{
    public interface ICouponService
    {

        Task<CouponViewModel> GetDiscountCoupon(string couponCode, string token);
    }

}

