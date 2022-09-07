using Microsoft.EntityFrameworkCore;
using VShopping.DiscountAPI.Model;

namespace VShopping.DiscountAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "VShopping_Promo_10",
                Discount = 10
            });

            model.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "VShopping_Promo_20",
                Discount = 20
            });
        }

    }
}
