using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToyWorld.Models;

namespace ToyWorld.Data.Configurations;

// Fluent API для таблицы "Заказы"
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
        builder.Property(o => o.ShippingAddress).HasMaxLength(500);

        // Статус заказа храним как строку, не число
        builder.Property(o => o.Status).HasConversion<string>().HasMaxLength(30);

        // Удаление заказа удаляет и его позиции
        builder.HasMany(o => o.OrderItems)
               .WithOne(oi => oi.Order)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

// Fluent API для таблицы "Позиции заказа"
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");

        // Товар, использующийся в заказах, удалять запрещено
        builder.HasOne(oi => oi.Product)
               .WithMany(p => p.OrderItems)
               .HasForeignKey(oi => oi.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

        // Subtotal — вычисляемое свойство, не маппим в БД
        builder.Ignore(oi => oi.Subtotal);
    }
}

// Fluent API для таблицы "Корзина"
public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => ci.Id);

        // Удаление товара очищает связанные записи в корзинах
        builder.HasOne(ci => ci.Product)
               .WithMany(p => p.CartItems)
               .HasForeignKey(ci => ci.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
