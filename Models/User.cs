using Microsoft.AspNetCore.Identity;

namespace Churn.Models
{
    public class User : IdentityUser
    {
        public List<Order>? Order { get; set; }

        public List<Cart>? Cart { get; set; }
    }
}