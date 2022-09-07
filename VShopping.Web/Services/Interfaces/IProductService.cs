using VShopping.Web.Models;

namespace VShopping.Web.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts(string token);
        Task<ProductViewModel> GetProductById(int id, string token);
        Task<ProductViewModel> CreateProduct(ProductViewModel productView, string token);
        Task<ProductViewModel> UpdateProduct(ProductViewModel productView, string token);
        Task<bool> DeleteProductById(int id, string token);

    }
}
