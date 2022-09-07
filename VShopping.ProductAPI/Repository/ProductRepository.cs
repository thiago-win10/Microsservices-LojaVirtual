﻿using Microsoft.EntityFrameworkCore;
using VShopping.ProductAPI.Context;
using VShopping.ProductAPI.Models;
using VShopping.ProductAPI.Repository.Interfaces;

namespace VShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.Include(x => x.Category)
                .Where(p => p.Id== id).FirstOrDefaultAsync();
        }

        public async Task<Product> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task<Product> Delete(int id)
        {
            var product = await GetById(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;

        }
    }
}
