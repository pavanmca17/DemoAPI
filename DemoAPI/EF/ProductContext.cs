using DemoAPI.Interface;
using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EF
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
          //  products = new DbSet<Product>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>();
            modelBuilder.Entity<Product>()
                .HasKey(c => c.ID);

            modelBuilder.Entity<TestEmployee>();
            modelBuilder.Entity<TestEmployee>()
                .HasKey(c => c.ID);


        }

        public DbSet<Product> products { get; set; }

    }
}