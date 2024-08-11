using CategoriesProductsAPI.Dtos;
using CategoriesProductsAPI.Models;
using CategoriesProductsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoriesProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<CategoryDto>>> GetCategories([FromQuery] PaginationParams paginationParams)
        {
            return await _categoryService.GetCategoriesAsync(paginationParams);
        }

        [HttpGet("withProductCount")]
        public async Task<ActionResult<IEnumerable<CategoryProductCountDto>>> GetCategoriesWithProductCount()
        {
            var categories = await _categoryService.GetCategoriesWithProductCountAsync();
            return Ok(categories);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            return await _categoryService.GetCategoryByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = await _categoryService.CreateCategoryAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CreateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}