using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Churn.Models
{
    public class Product


    {

        public enum ProductAnnualFees
        {
            Low,          
            Standard,     
            Premium      
        }


        public int Id { get; set; }

        [Required()]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required()]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Display(Name = "Product Description")]

        public string? Description { get; set; }

        [Required(), Range(0.01, 50)]
        [Display(Name = "Interest Rate")]


        public double InterestRate { get; set; }

        [Required(), Range(0.01, 40)]
        [Display(Name = "Term Length")]

        public double TermLength { get; set; }
        public string? Photo { get ; set; }

        [Display(Name = "Credit Limit/Minimum Investment")]
        public decimal? Limit { get; set; }

        [Required()]

        public ProductAnnualFees AnnualFees { get; set; }

        [Required()]

        [Display(Name = "Annual Fee (Paid at Checkout)")]
        public decimal AnnualFee { get; set; }

        public Category? Category { get; set; } //parent reference

    }
}
