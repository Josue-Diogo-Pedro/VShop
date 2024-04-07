using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VShop.CartApi.Models;

namespace VShop.CartApi.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.ProductId);

        builder.Property(p => p.ProductId).ValueGeneratedNever();
        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.Property(p => p.ImageURL).HasMaxLength(255).IsRequired();
        builder.Property(p => p.CategoryName).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Price).HasPrecision(12, 2);

    }
}
