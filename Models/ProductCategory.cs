using System.ComponentModel.DataAnnotations;

namespace EasyCommerce.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }
        public ProductCategory ParentCategory { get; set; }

        public ICollection<ProductCategory> SubCategories { get; set; } = new List<ProductCategory>();

        // Relacionamento um-para-muitos
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
