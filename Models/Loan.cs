using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Churn.Models
{
    public class Loan : Product
    {
        [Required(ErrorMessage = "You must provide the loan interest rate.")]
        [Display(Name = "Interest Rate")]
        public double InterestRate { get; set; }

        [Required(ErrorMessage = "You must provide the loan amount.")]
        [Display(Name = "Loan Amount")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LoanAmount { get; set; }

        [Required(ErrorMessage = "You must provide the loan term in months.")]
        [Display(Name = "Loan Term (Months)")]
        public int LoanTermMonths { get; set; }

        [Display(Name = "Collateral")]
        public string Collateral { get; set; }
    }
}
