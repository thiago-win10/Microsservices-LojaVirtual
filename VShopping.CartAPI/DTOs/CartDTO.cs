using VShopping.CartAPI.Models;

namespace VShopping.CartAPI.DTOs
{
    public class CartDTO
    {
        public CartHeader CartHeader { get; set; } = new CartHeader();
        public IEnumerable<CartItem> CartItems { get; set; } = Enumerable.Empty<CartItem>();

    }
}
