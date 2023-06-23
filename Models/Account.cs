using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Churn.Models
{
    public class Account : Product
    {
        [Required(ErrorMessage = "You must provide the account type.")]
        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "You must provide the minimum balance.")]
        [Display(Name = "Minimum Balance")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MinimumBalance { get; set; }

        [Display(Name = "Interest Rate")]
        public double InterestRate { get; set; }

        [Required(ErrorMessage = "You must provide the maximum withdrawal limit.")]
        [Display(Name = "Maximum Withdrawal Limit")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MaximumWithdrawalLimit { get; set; }
    }
}
