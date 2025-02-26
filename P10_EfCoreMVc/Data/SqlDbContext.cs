using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.DomainModel;
using WebApplication1.Models.JunctionModels;

namespace WebApplication1.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Review> Reviews {get ; set;}
        public DbSet<CartProduct> CartProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Order>()
                .HasOne(o => o.Buyer)    // order has one buyer
                .WithMany(b => b.Orders)   // buyer can have many orders
                .HasForeignKey(o => o.BuyerId)   //order has BuyerId as a foreign key 
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Product>()
                .HasOne(p => p.Seller)       // product has one seller
                .WithMany(s => s.Products)    // seller has many products
                .HasForeignKey(p => p.SellerId) // product has SellerId  as foreign key
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Buyer)    // address belongs to one buyer
                .WithMany(b => b.Addresses)   // buyer has many addresses
                .HasForeignKey(a => a.BuyerId) // addres has buyer  Id as foreign key
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Cart>()
               .HasOne(c => c.Buyer)    // cart belongs to one buyer
               .WithOne(b => b.Cart)   // buyer has only one cart
               .HasForeignKey<Cart>(c => c.BuyerId)  // cart has Buyer Id as foreign key
               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Review>()
               .HasOne(r => r.User)       // review belongs to one user
               .WithMany(u => u.Reviews)
               .HasForeignKey(r => r.UserId);


            modelBuilder.Entity<Review>()
               .HasOne(r => r.Product)       // review belongs to one product
               .WithMany(p => p.Reviews)
               .HasForeignKey(r => r.ProductId);


            //    many to many relationShip 

            modelBuilder.Entity<CartProduct>()
                .HasKey(cp => cp.CartProductId);


            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Cart)
                .WithMany(c => c.Products)     // cart have many products
                .HasForeignKey(cp => cp.CartId);

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.Carts)         //product belongs to many carts
                .HasForeignKey(cp => cp.ProductId);

        }
    }
}