using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToyWorld.Models;

namespace ToyWorld.Data.Configurations;

// Fluent API для таблицы "Характеристики товара"
public class ProductDetailsConfiguration : IEntityTypeConfiguration<ProductDetails>
{
    public void Configure(EntityTypeBuilder<ProductDetails> builder)
    {
        builder.HasKey(d => d.Id);

        // Ограничения длины строковых полей
        builder.Property(d => d.Manufacturer).HasMaxLength(150);
        builder.Property(d => d.CountryOfOrigin).HasMaxLength(100);
        builder.Property(d => d.Dimensions).HasMaxLength(100);

        // Гарантирует один набор характеристик на товар
        builder.HasIndex(d => d.ProductId).IsUnique();
    }
}
