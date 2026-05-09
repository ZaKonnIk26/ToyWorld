using Microsoft.EntityFrameworkCore;
using ToyWorld.Data;
using ToyWorld.Models;

namespace ToyWorld.Services;

// Бизнес-сервис корзины покупателя
public interface ICartService
{
    Task<IEnumerable<CartItem>> GetCartAsync(int customerId);
    Task AddToCartAsync(int customerId, int productId, int quantity = 1);
    Task UpdateQuantityAsync(int cartItemId, int quantity);
    Task RemoveFromCartAsync(int cartItemId);
    Task ClearCartAsync(int customerId);
    Task<decimal> GetTotalAsync(int customerId);
}

public class CartService : ICartService
{
    private readonly ToyDbContext _toys;

    public CartService(ToyDbContext context) => _toys = context;

    // Содержимое корзины с подгрузкой товаров
    public async Task<IEnumerable<CartItem>> GetCartAsync(int customerId)
        => await _toys.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.CustomerId == customerId)
            .ToListAsync();

    // Если товар уже в корзине — увеличиваем количество, иначе добавляем
    public async Task AddToCartAsync(int customerId, int productId, int quantity = 1)
    {
        var existing = await _toys.CartItems
            .FirstOrDefaultAsync(ci => ci.CustomerId == customerId && ci.ProductId == productId);

        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            await _toys.CartItems.AddAsync(new CartItem
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity
            });
        }
        await _toys.SaveChangesAsync();
    }

    // Количество <= 0 удаляет позицию из корзины
    public async Task UpdateQuantityAsync(int cartItemId, int quantity)
    {
        var item = await _toys.CartItems.FindAsync(cartItemId);
        if (item is null) return;

        if (quantity <= 0)
            _toys.CartItems.Remove(item);
        else
            item.Quantity = quantity;

        await _toys.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(int cartItemId)
    {
        var item = await _toys.CartItems.FindAsync(cartItemId);
        if (item is null) return;
        _toys.CartItems.Remove(item);
        await _toys.SaveChangesAsync();
    }

    public async Task ClearCartAsync(int customerId)
    {
        var items = _toys.CartItems.Where(ci => ci.CustomerId == customerId);
        _toys.CartItems.RemoveRange(items);
        await _toys.SaveChangesAsync();
    }

    // Общая стоимость корзины
    public async Task<decimal> GetTotalAsync(int customerId)
        => await _toys.CartItems
            .Where(ci => ci.CustomerId == customerId)
            .SumAsync(ci => ci.Quantity * ci.Product.Price);
}
