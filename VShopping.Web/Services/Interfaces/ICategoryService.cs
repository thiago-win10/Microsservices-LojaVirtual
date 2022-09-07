using VShopping.Web.Models;

namespace VShopping.Web.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategory(string token);
    }
}
