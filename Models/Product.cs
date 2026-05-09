namespace ToyWorld.Models;

// Товар в магазине
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Внешний ключ и навигация на категорию (N:1)
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // Дополнительные характеристики (1:1)
    public ProductDetails? Details { get; set; }

    // Связи с позициями заказов и корзинами
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
