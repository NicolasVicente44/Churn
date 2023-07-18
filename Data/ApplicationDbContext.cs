using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Churn.Models;

namespace Churn.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

            public DbSet<Product>? Products { get; set; }

            public DbSet<CreditCard>? CreditCards { get; set; }
            public DbSet<Mortgage>? Mortgages { get; set; }
            public DbSet<Loan>? Loans { get; set; }
            public DbSet<Account>? Accounts { get; set; }
            public DbSet<Investment>? Investments { get; set; }
            public DbSet<Cart> Carts { get; set; }
            public DbSet<CartItem> CartItems { get; set; }
            public DbSet<Order> Orders { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Order>()
                .HasOne(o => o.Cart)
                .WithMany()
                .HasForeignKey(o => o.CartId)
                .OnDelete(DeleteBehavior.Restrict);


                 builder.Entity<CartItem>() 
                    .HasOne(o => o.Cart)
                    .WithMany(o => o.CartItems)
                    .HasForeignKey(o => o.CartId)
                    .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
} 