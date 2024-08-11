using CategoriesProductsAPI.Dtos;
using CategoriesProductsAPI.Models;

namespace CategoriesProductsAPI.Services
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryDto>> GetCategoriesAsync(PaginationParams paginationParams);
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(int id, CreateCategoryDto updateCategoryDto);
        Task DeleteCategoryAsync(int id);
        Task<IEnumerable<CategoryProductCountDto>> GetCategoriesWithProductCountAsync();
    }
}