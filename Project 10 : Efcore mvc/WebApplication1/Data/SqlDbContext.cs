using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class SqlDbContext : DbContext
{


   public SqlDbContext(DbContextOptions<SqlDbContext> options)
        : base(options)
    {
    }

          public DbSet<User> Users { get; set; }
          public DbSet<Product> Products { get; set; }

}
