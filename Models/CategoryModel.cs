using System.ComponentModel.DataAnnotations;

namespace CategoriesProductsAPI.Models{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new List<Product>();
    }
}