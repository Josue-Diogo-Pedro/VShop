using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VShop.DiscountApi.Models;

namespace VShop.DiscountApi.Configurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("Coupons");
        builder.HasKey(key => key.CouponId);

        builder.Property(p => p.CouponCode).HasColumnType("varchar(30)");
        builder.Property(p => p.Discount).HasColumnType("decimal(10,2)");

        builder.HasIndex(p => p.CouponCode).IsUnique();
    }
}
