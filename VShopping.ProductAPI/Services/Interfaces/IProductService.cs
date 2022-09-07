using VShopping.ProductAPI.DTOs;

namespace VShopping.ProductAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProdcuts();
        Task<ProductDTO> GetProductById(int id);
        Task AddProduct(ProductDTO productDTO);
        Task UpdateProduct(ProductDTO productDTO);
        Task RemoveProduct(int id);

    }
}
