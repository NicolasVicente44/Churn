using System.ComponentModel.DataAnnotations;

namespace Churn.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must provide a product name.")]
        [MaxLength(200)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Product Description")]
        public string? Description { get; set; }

       
    }
}
