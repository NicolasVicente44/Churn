using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Churn.Models
{
    public class CreditCard : Product
    {
        [Required(ErrorMessage = "You must provide the credit card's interest rate.")]
        [Display(Name = "Interest Rate")]
        public double InterestRate { get; set; }

        [Required(ErrorMessage = "You must provide the minimum payment amount.")]
        [Display(Name = "Minimum Payment Amount")]
        public double MinimumPaymentAmount { get; set; }

        [Display(Name = "Annual Fee")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AnnualFee { get; set; }

        [Display(Name = "Balance Transfer Amount")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BalanceTransferAmount { get; set; }
    }
}
