using System.ComponentModel.DataAnnotations;

namespace CategoriesProductsAPI.Dtos{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
    }

    public class CreateCategoryDto
    {
        [Required]
        public string NameEn { get; set; } = string.Empty;
        [Required]
        public string NameAr { get; set; } = string.Empty;
    }
    
    public class CategoryProductCountDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public int ProductCount { get; set; }
    }
}
