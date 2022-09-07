using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShopping.DiscountAPI.DTOs;
using VShopping.DiscountAPI.Repository;

namespace VShopping.DiscountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private ICouponRepository _couponRepository;
        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository?? 
                throw new ArgumentNullException(nameof(couponRepository));
        }
        [HttpGet("{couponCode}")]
        [Authorize]
        public async Task<ActionResult<CouponDTO>> GetDiscountCouponByCode(string couponCode)
        {
            var coupon = await _couponRepository.GetCouponByCode(couponCode);

            if(coupon is null)
            {
                return NotFound($"Coupon Code: {couponCode} not found");
            }
            return Ok(coupon);  
        }
    }
}
