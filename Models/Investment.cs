using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Churn.Models
{
    public class Investment : Product
    {
        [Required(ErrorMessage = "You must provide the investment type.")]
        [Display(Name = "Investment Type")]
        public string InvestmentType { get; set; }

        [Required(ErrorMessage = "You must provide the minimum investment amount.")]
        [Display(Name = "Minimum Investment Amount")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MinimumInvestmentAmount { get; set; }

        [Display(Name = "Expected Return Rate")]
        public double ExpectedReturnRate { get; set; }

        [Required(ErrorMessage = "You must provide the investment term.")]
        [Display(Name = "Investment Term")]
        public int InvestmentTerm { get; set; }
    }
}
