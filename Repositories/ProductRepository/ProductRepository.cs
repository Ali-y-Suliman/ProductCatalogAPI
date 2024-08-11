using CategoriesProductsAPI.Data;
using CategoriesProductsAPI.ErrorException;
using CategoriesProductsAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CategoriesProductsAPI.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) { }

        
        public async Task<Product?> GetByIsbnAsync(string isbn)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ISBN == isbn);
        }
        
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string? search, int? categoryId)
        {
            
            var query = _context.Products.Include(p => p.Categories).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search) || p.ISBN.Contains(search));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.Categories.Any(c => c.Id == categoryId.Value));
            }

            query = query.OrderByDescending(p => p.Id);
            
            return await query.ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string? search, int? categoryId)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search) || p.ISBN.Contains(search));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.Categories.Any(c => c.Id == categoryId.Value));
            }

            return await query.CountAsync();
        }
    }
}