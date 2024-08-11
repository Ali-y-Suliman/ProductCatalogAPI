
using CategoriesProductsAPI.Dtos;
using CategoriesProductsAPI.Models;

namespace CategoriesProductsAPI.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Category>> GetCategoriesByIdsAsync(IEnumerable<int> ids);
        Task<IEnumerable<CategoryProductCountDto>> GetAllWithProductCountAsync();
    }
}