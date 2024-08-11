using CategoriesProductsAPI.Data;
using CategoriesProductsAPI.Dtos;
using CategoriesProductsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoriesProductsAPI.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetCategoriesByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Categories.Where(c => ids.Contains(c.Id)).ToListAsync();
        }
        public async Task<IEnumerable<CategoryProductCountDto>> GetAllWithProductCountAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryProductCountDto
                {
                    Id = c.Id,
                    NameEn = c.NameEn,
                    NameAr = c.NameAr,
                    ProductCount = c.Products.Count()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            
            var query = _context.Categories.AsQueryable();
            query = query.OrderByDescending(p => p.Id);
            
            return await query.ToListAsync();

        }
    }
}