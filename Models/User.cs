using Microsoft.AspNetCore.Identity;

namespace Churn.Models
{
    public class User : IdentityUser
    {
        public List<Order>? Orders { get; set; }

        public List<Cart>? Carts { get; set; }
    }
}