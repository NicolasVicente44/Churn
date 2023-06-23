using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Churn.Models
{
    public class Mortgage : Product
    {
        [Required(ErrorMessage = "You must provide the mortgage interest rate.")]
        [Display(Name = "Interest Rate")]
        public double InterestRate { get; set; }

        [Required(ErrorMessage = "You must provide the mortgage amount.")]
        [Display(Name = "Mortgage Amount")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MortgageAmount { get; set; }

        [Required(ErrorMessage = "You must provide the mortgage term in years.")]
        [Display(Name = "Mortgage Term (Years)")]
        public int MortgageTermYears { get; set; }

        [Display(Name = "Down Payment")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DownPayment { get; set; }
    }
}
