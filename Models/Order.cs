namespace ToyWorld.Models;

// Возможные статусы заказа
public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

// Заказ покупателя
public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    public string? ShippingAddress { get; set; }

    // Внешний ключ на покупателя (N:1)
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    // Позиции заказа (связь N:N с товарами через OrderItem)
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
