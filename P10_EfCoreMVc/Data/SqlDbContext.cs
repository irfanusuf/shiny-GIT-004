using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; } 


        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)    // order has one user
                .WithMany(u => u.Orders)   // user can have many orders
                .HasForeignKey(o => o.UserId);    //order has userid as a foreign key 


            modelBuilder.Entity<Product>().HasOne(u => u.Seller );

        }
    }
}