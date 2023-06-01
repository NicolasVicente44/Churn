using Churn.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Churn.Models
{
    public enum TermLength
    {
        ShortTerm,
        MediumTerm,
        LongTerm
    }

    public class Product
    {
        public int Id { get; set; }

        [Required()]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required()]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required(), Range(0.01, 1000)]
        public double InterestRate { get; set; }

        [Required()]
        public TermLength TermLength { get; set; }

        public string? Photo { get; set; }
        public Category? Category { get; set; }
    }
}
