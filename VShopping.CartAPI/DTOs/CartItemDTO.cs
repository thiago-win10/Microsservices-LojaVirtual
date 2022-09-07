using VShopping.CartAPI.Models;

namespace VShopping.CartAPI.DTOs
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 1;
        public int ProductId { get; set; }
        public int CartHeaderId { get; set; }
        public ProductDTO Product { get; set; }
        public CartHeaderDTO CartHeader { get; set; }
    }
}
