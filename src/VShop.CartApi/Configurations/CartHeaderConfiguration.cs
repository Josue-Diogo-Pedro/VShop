using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VShop.CartApi.Models;

namespace VShop.CartApi.Configurations;

public class CartHeaderConfiguration : IEntityTypeConfiguration<CartHeader>
{
    public void Configure(EntityTypeBuilder<CartHeader> builder)
    {
        builder.ToTable("CartHeaders");
        builder.HasKey(k => k.CartHeaderId);

        builder.Property(p => p.UserId).HasMaxLength(255).IsRequired();
        builder.Property(p => p.CouponCode).HasMaxLength(100);
    }
}
