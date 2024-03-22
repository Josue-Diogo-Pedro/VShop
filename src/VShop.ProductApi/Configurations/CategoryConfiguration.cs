using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(id => id.CategoryId);

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.HasMany(p => p.Products)
               .WithOne(p => p.Category)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Category
            {
                CategoryId = 1,
                Name = "Material Escolar"
            },
            new Category
            {
                CategoryId = 2,
                Name = "Acessórios"
            }
        );
    }
}
