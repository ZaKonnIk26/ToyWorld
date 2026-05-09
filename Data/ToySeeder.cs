using Microsoft.EntityFrameworkCore;
using ToyWorld.Models;

namespace ToyWorld.Data;

// Начальные данные ToyWorld — магазин игрушек
public static class ToySeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Конструкторы", Description = "LEGO, магнитные и деревянные" },
            new Category { Id = 2, Name = "Куклы", Description = "Куклы и аксессуары к ним" },
            new Category { Id = 3, Name = "Машинки", Description = "Радиоуправляемые и коллекционные" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "LEGO City Полицейский участок", Description = "743 детали, 6+", Price = 8990m, Stock = 12, CategoryId = 1, ImageUrl = "https://placehold.co/400x400/d32f2f/fbc02d?text=LEGO+City", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 2, Name = "Магнитный конструктор 100 деталей", Description = "Развивающий, 3+", Price = 3490m, Stock = 25, CategoryId = 1, ImageUrl = "https://placehold.co/400x400/fbc02d/d32f2f?text=Magnetic", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 3, Name = "Кукла Barbie Принцесса", Description = "С платьем и расчёской", Price = 2990m, Stock = 30, CategoryId = 2, ImageUrl = "https://placehold.co/400x400/d32f2f/fbc02d?text=Barbie", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 4, Name = "Кукла L.O.L. Surprise", Description = "Мини-кукла с сюрпризами", Price = 1990m, Stock = 40, CategoryId = 2, ImageUrl = "https://placehold.co/400x400/fbc02d/d32f2f?text=LOL", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 5, Name = "Машинка р/у Ferrari", Description = "Масштаб 1:14, до 12 км/ч", Price = 4990m, Stock = 15, CategoryId = 3, ImageUrl = "https://placehold.co/400x400/d32f2f/fbc02d?text=Ferrari+RC", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 6, Name = "Hot Wheels набор 5 машинок", Description = "Коллекционный набор", Price = 1290m, Stock = 50, CategoryId = 3, ImageUrl = "https://placehold.co/400x400/fbc02d/d32f2f?text=HotWheels", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

        modelBuilder.Entity<ProductDetails>().HasData(
            new ProductDetails { Id = 1, ProductId = 1, Manufacturer = "LEGO", CountryOfOrigin = "Дания", Weight = 1.1, Dimensions = "48x28x9 см", WarrantyMonths = 0 },
            new ProductDetails { Id = 2, ProductId = 3, Manufacturer = "Mattel", CountryOfOrigin = "Китай", Weight = 0.3, Dimensions = "30x10x6 см", WarrantyMonths = 0 },
            new ProductDetails { Id = 3, ProductId = 5, Manufacturer = "Rastar", CountryOfOrigin = "Китай", Weight = 0.6, Dimensions = "30x14x9 см", WarrantyMonths = 12 }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, FullName = "Олеся Малышкина", Email = "olesya@toyworld.local", Phone = "+7-944-000-00-05", Address = "Новосибирск, Красный пр., 100", RegisteredAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
