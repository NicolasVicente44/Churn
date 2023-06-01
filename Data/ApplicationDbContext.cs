using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Churn.Models;

namespace Churn.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        
        public DbSet<Churn.Models.Category>? Category { get; set; }
        public DbSet<Churn.Models.Product>? Product { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
} 