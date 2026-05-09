namespace ToyWorld.Models;

// Подробные характеристики товара (связь 1:1 с Product)
public class ProductDetails
{
    public int Id { get; set; }
    public string? Manufacturer { get; set; }
    public string? CountryOfOrigin { get; set; }
    public double? Weight { get; set; }
    public string? Dimensions { get; set; }
    public int WarrantyMonths { get; set; }

    // Внешний ключ и навигация на товар
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
