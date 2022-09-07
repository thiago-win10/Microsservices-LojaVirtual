using Microsoft.EntityFrameworkCore;
using VShopping.ProductAPI.Models;

namespace VShopping.ProductAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        //Fluen API

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //category
            mb.Entity<Category>().HasKey(c => c.CategoryId);
            mb.Entity<Category>().Property(c => c.Name).HasMaxLength(100).IsRequired();

            //product
            mb.Entity<Product>().HasKey(p => p.Id);
            mb.Entity<Product>().Property(p => p.Name).HasMaxLength(100).IsRequired();
            mb.Entity<Product>().Property(p => p.Description).HasMaxLength(255).IsRequired();
            mb.Entity<Product>().Property(p => p.Price).HasPrecision(12, 2).IsRequired();
            mb.Entity<Product>().Property(p => p.ImageURL).HasMaxLength(255);

            //Relacionamentos entre tabelas

            mb.Entity<Category>().HasMany(g => g.Products)
                .WithOne(c => c.Category)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //Populando BD Categories

            mb.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Material Escolar",
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Acessórios",
                });
        }

    }
}
