using CategoriesProductsAPI.Models;

namespace CategoriesProductsAPI.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsAsync(string? search, int? categoryId);
        Task<Product?> GetByIsbnAsync(string isbn);
        Task<Product?> GetProductByIdAsync(int id);
    }
}