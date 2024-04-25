using Microsoft.EntityFrameworkCore;
using VShop.DiscountApi.Models;

namespace VShop.DiscountApi.Context;

public class DiscountApiDbContext : DbContext
{
    public DbSet<Coupon>? Coupons { get; set; }

	public DiscountApiDbContext(DbContextOptions<DiscountApiDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DiscountApiDbContext).Assembly);
    }
}
