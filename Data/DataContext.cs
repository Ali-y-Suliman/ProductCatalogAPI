using CategoriesProductsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoriesProductsAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2); 

            modelBuilder.Entity<Product>()
                .HasIndex(e => e.ISBN)
                .IsUnique()
                .HasDatabaseName("UK_Product_ISBN");

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity(j => j.ToTable("ProductCategories"));

        }
    }
}
