namespace ToyWorld.Models;

// Категория товара (Электроника, Одежда, Книги и т.д.)
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Связь "один ко многим" с товарами
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
