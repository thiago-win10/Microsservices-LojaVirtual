using VShopping.ProductAPI.DTOs;

namespace VShopping.ProductAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<IEnumerable<CategoryDTO>> GetCategoriesProducts();
        Task <CategoryDTO> GetCategorieById(int id);
        Task AddCategory(CategoryDTO categoryDTO);
        Task UpdateCategory(CategoryDTO categoryDTO);
        Task RemoveCategory(int id);

    }
}
