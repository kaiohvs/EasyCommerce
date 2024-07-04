using System.ComponentModel.DataAnnotations;

namespace EasyCommerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [StringLength(500, ErrorMessage = "O campo Descrição não pode ter mais de 500 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo Preço é obrigatório.")]
        [Range(0.01, 1000000, ErrorMessage = "O campo Preço deve estar entre 0.01 e 1,000,000.")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)] // Formatação de exibição para formato monetário
        public decimal Price { get; set; }

        // Chave estrangeira para a categoria
        public int ProductCategoryId { get; set; }
        public ProductCategory ?ProductCategory { get; set; }

    }
}
