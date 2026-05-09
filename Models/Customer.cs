namespace ToyWorld.Models;

// Покупатель магазина
public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    // Заказы и корзина покупателя (1:N)
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
