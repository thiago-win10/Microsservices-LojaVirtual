using VShopping.ProductAPI.Models;

namespace VShopping.ProductAPI.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetCategoryProducts();
        Task<Category> GetById(int id);
        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task<Category> Delete(int id);


    }
}
