using AutoMapper;
using CategoriesProductsAPI.Dtos;
using CategoriesProductsAPI.ErrorException;
using CategoriesProductsAPI.Models;
using CategoriesProductsAPI.Repository;

namespace CategoriesProductsAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedResult<ProductDto>> GetProductsAsync(PaginationParams paginationParams, string? search, int? categoryId)
        {
            try
            {
                var products = await _productRepository.GetProductsAsync(search, categoryId);
                
                var totalCount = products.Count();
                var totalPages = (int)Math.Ceiling(totalCount / (double)paginationParams.PageSize);

                var paginatedProducts = products
                    .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                    .Take(paginationParams.PageSize);

                var productDtos = _mapper.Map<List<ProductDto>>(paginatedProducts);

                return new PagedResult<ProductDto>
                {
                    Items = productDtos,
                    PageNumber = paginationParams.PageNumber,
                    PageSize = paginationParams.PageSize,
                    TotalPages = totalPages,
                    TotalCount = totalCount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching products");
                throw new AppException(ex.Message, ex);
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {   
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                    throw new KeyNotFoundException("Product not found");

                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting product");
                throw new AppException(ex.Message, ex);
            }
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            try
            {
                if (!IsValidISBN(createProductDto.ISBN))
                    throw new AppException("Invalid ISBN");

                if (await _productRepository.GetByIsbnAsync(createProductDto.ISBN) != null)
                        {
                            throw new AppException("A product with this ISBN already exists.");
                        }

                var product = _mapper.Map<Product>(createProductDto);

                // Fetch only the categories that are in the createProductDto.CategoryIds
                var categories = await _categoryRepository.GetCategoriesByIdsAsync(createProductDto.CategoryIds);

                // Check if all requested category IDs were found
                if (categories.Count() != createProductDto.CategoryIds.Count())
                {
                    var missingCategoryIds = createProductDto.CategoryIds.Except(categories.Select(c => c.Id));
                    throw new AppException($"Categories not found: {string.Join(", ", missingCategoryIds)}");
                }

                product.Categories = categories.ToList();
                await _productRepository.AddAsync(product);

                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product");
                throw new AppException(ex.Message, ex);
            }
        }

        public async Task UpdateProductAsync(int id, CreateProductDto updateProductDto)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                    throw new KeyNotFoundException("Product not found");

                if (!IsValidISBN(updateProductDto.ISBN))
                    throw new AppException("Invalid ISBN");

                if (product.ISBN != updateProductDto.ISBN)
                {
                    if (await _productRepository.GetByIsbnAsync(updateProductDto.ISBN) != null)
                    {
                        throw new AppException("A product with this ISBN already exists.");
                    }
                }

                _mapper.Map(updateProductDto, product);
                
                // Get the categories to be added and removed
                var updatedCategories = await _categoryRepository.GetCategoriesByIdsAsync(updateProductDto.CategoryIds);

                // Check if all requested category IDs were found
                if (updatedCategories.Count() != updateProductDto.CategoryIds.Count())
                {
                    var missingCategoryIds = updateProductDto.CategoryIds.Except(updatedCategories.Select(c => c.Id));
                    throw new AppException($"Categories not found: {string.Join(", ", missingCategoryIds)}");
                }

                // Replace the entire Categories collection
                product.Categories.Clear();
                foreach (var category in updatedCategories)
                {
                    product.Categories.Add(category);
                }

                await _productRepository.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the product");
                throw new AppException(ex.Message, ex);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                    throw new KeyNotFoundException("Product not found");

                await _productRepository.DeleteAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the product");
                throw new AppException(ex.Message, ex);
            }
        }

        private bool IsValidISBN(string isbn)
        {
            return !string.IsNullOrEmpty(isbn) && isbn.All(char.IsDigit);
        }
    }
}