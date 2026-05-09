namespace ToyWorld.Models;

// Одна позиция в заказе: связывает заказ с товаром и хранит цену на момент покупки
public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    // Сумма по позиции (вычисляется, в БД не хранится)
    public decimal Subtotal => Quantity * UnitPrice;
}
