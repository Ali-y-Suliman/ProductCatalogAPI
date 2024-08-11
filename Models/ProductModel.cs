using System.ComponentModel.DataAnnotations;

namespace CategoriesProductsAPI.Models{
    public class Product
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
        
            public string ISBN { get; set; } = string.Empty;
            public List<Category> Categories { get; set; } = new List<Category>();
        }
}