using AutoMapper;
using CategoriesProductsAPI.Dtos;
using CategoriesProductsAPI.ErrorException;
using CategoriesProductsAPI.Models;
using CategoriesProductsAPI.Repository;

namespace CategoriesProductsAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(createCategoryDto);

                await _categoryRepository.AddAsync(category);

                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating category");
                throw new AppException(ex.Message, ex);
            }
        }

        public async Task<PagedResult<CategoryDto>> GetCategoriesAsync(PaginationParams paginationParams)
        {
            try
            {
                var categories = await _categoryRepository.GetCategoriesAsync();

                var totalCount = categories.Count();
                var totalPages = (int)Math.Ceiling(totalCount / (double)paginationParams.PageSize);

                var categoryDtos = _mapper.Map<List<CategoryDto>>(categories
                    .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                    .Take(paginationParams.PageSize));

                return new PagedResult<CategoryDto>
                {
                    Items = categoryDtos,
                    PageNumber = paginationParams.PageNumber,
                    PageSize = paginationParams.PageSize,
                    TotalPages = totalPages,
                    TotalCount = totalCount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching categories");
                throw new AppException(ex.Message, ex);
            }
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            try {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                    throw new KeyNotFoundException("Category not found");

                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting category");
                throw new AppException(ex.Message, ex);
            }
        }

        public async Task UpdateCategoryAsync(int id, CreateCategoryDto updateCategoryDto)
        {
            try {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                    throw new KeyNotFoundException("Category not found");

                _mapper.Map(updateCategoryDto, category);

                await _categoryRepository.UpdateAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating category");
                throw new AppException(ex.Message, ex);
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            try {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                    throw new KeyNotFoundException("Category not found");

                await _categoryRepository.DeleteAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting category");
                throw new AppException(ex.Message, ex);
            }
        }
        public async Task<IEnumerable<CategoryProductCountDto>> GetCategoriesWithProductCountAsync()
        {
            try {
                var categories = await _categoryRepository.GetAllWithProductCountAsync();
                return _mapper.Map<IEnumerable<CategoryProductCountDto>>(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting the categories with products count");
                throw new AppException(ex.Message, ex);
            }
        }
    }
}