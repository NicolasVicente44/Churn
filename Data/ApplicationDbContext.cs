using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Churn.Models;

namespace Churn.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

            public DbSet<Product>? Products { get; set; }

            public DbSet<CreditCard>? CreditCards { get; set; }
            public DbSet<Mortgage>? Mortgages { get; set; }
            public DbSet<Loan>? Loans { get; set; }
            public DbSet<Account>? Accounts { get; set; }
            public DbSet<Investment>? Investments { get; set; }
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
} 