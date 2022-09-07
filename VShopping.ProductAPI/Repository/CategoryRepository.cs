using Microsoft.EntityFrameworkCore;
using VShopping.ProductAPI.Context;
using VShopping.ProductAPI.Models;
using VShopping.ProductAPI.Repository.Interfaces;

namespace VShopping.ProductAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetCategoryProducts()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
        }

        public async Task<Category> Create(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<Category> Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;

        }

        public async Task<Category> Delete(int id)
        {
            var category = await GetById(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;

        }

    }
}
