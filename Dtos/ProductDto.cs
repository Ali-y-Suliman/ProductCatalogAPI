using System.ComponentModel.DataAnnotations;

namespace CategoriesProductsAPI.Dtos{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }

    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ISBN { get; set; } = string.Empty;
        [Required]
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}