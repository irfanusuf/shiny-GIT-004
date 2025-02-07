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
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Cart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Order>()
                .HasOne(o => o.Buyer)    // order has one buyer
                .WithMany(u => u.Orders)   // user can have many orders
                .HasForeignKey(o => o.BuyerId);    //order has BuyerId as a foreign key 


            modelBuilder.Entity<Product>()
                .HasOne(p => p.Seller)       // product has one seller
                .WithMany(s => s.Products)    // seller has many products
                .HasForeignKey(p => p.SellerId);  // product has SellerId  as foreign key

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Buyer)    // address belongs to one buyer
                .WithMany(b => b.Addresses)   // buyer has many addresses
                .HasForeignKey(a => a.AddressId);   // addres has addres Id as foreign key

             modelBuilder.Entity<Cart>()
                .HasOne(c => c.Buyer)    // cart belongs to one buyer
                .WithOne(b => b.Cart)   // buyer has only one cart
                .HasForeignKey<Cart>(c => c.BuyerId);   // cart has Buyer Id as foreign key
        }
    }
}