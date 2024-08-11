using CategoriesProductsAPI.Dtos;
using CategoriesProductsAPI.Models;

namespace CategoriesProductsAPI.Services
{
    public interface IProductService
    {
        Task<PagedResult<ProductDto>> GetProductsAsync(PaginationParams paginationParams, string? search, int? categoryId);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(int id, CreateProductDto updateProductDto);
        Task DeleteProductAsync(int id);
    }
}