using Product.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Product.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
    }
}