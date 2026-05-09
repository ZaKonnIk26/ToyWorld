using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToyWorld.Models;

namespace ToyWorld.Data.Configurations;

// Fluent API для таблицы "Покупатели"
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.FullName).IsRequired().HasMaxLength(150);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Phone).HasMaxLength(30);
        builder.Property(c => c.Address).HasMaxLength(500);

        // Email уникальный
        builder.HasIndex(c => c.Email).IsUnique();

        // Покупателя с заказами удалить нельзя
        builder.HasMany(c => c.Orders)
               .WithOne(o => o.Customer)
               .HasForeignKey(o => o.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        // Корзина удаляется вместе с покупателем
        builder.HasMany(c => c.CartItems)
               .WithOne(ci => ci.Customer)
               .HasForeignKey(ci => ci.CustomerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
