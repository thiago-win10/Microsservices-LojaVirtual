using VShopping.ProductAPI.Models;

namespace VShopping.ProductAPI.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<Product> Delete(int id);


    }
}
