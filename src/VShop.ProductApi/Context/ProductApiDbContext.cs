using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context;

public class ProductApiDbContext : DbContext
{
    public DbSet<Product>? Products { get; set; }
    public DbSet<Category>? Categories { get; set; }

    public ProductApiDbContext(DbContextOptions<ProductApiDbContext> options) : base(options) { }
}
