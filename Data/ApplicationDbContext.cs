﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Churn.Models;
using System.Reflection.Emit;

namespace Churn.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }

          
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