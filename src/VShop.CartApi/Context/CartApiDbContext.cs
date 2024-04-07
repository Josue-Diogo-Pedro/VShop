using Microsoft.EntityFrameworkCore;
using VShop.CartApi.Models;

namespace VShop.CartApi.Context;

public class CartApiDbContext : DbContext
{
    public DbSet<Product>? Products { get; set; }
    public DbSet<CartHeader> CartHeader { get; set; }
    public DbSet<Cart> Carts { get; set; }

    public CartApiDbContext(DbContextOptions<CartApiDbContext> options): base(options) { }
}
