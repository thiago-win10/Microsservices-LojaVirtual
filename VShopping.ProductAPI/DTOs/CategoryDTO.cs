using System.ComponentModel.DataAnnotations;
using VShopping.ProductAPI.Models;

namespace VShopping.ProductAPI.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "The name is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
