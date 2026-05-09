using ToyWorld.Models;
using ToyWorld.Repositories;

namespace ToyWorld.Services;

// Бизнес-сервис для работы с товарами
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _toyRepo;

    public ProductService(IProductRepository repo) => _toyRepo = repo;

    public Task<IEnumerable<Product>> GetAllAsync() => _toyRepo.GetAllWithCategoryAsync();
    public Task<Product?> GetByIdAsync(int id) => _toyRepo.GetByIdWithDetailsAsync(id);
    public Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId) => _toyRepo.GetByCategoryAsync(categoryId);

    public async Task CreateAsync(Product product)
    {
        await _toyRepo.AddAsync(product);
        await _toyRepo.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _toyRepo.Update(product);
        await _toyRepo.SaveChangesAsync();
    }

    // Удаление: сначала ищем сущность, потом удаляем
    public async Task DeleteAsync(int id)
    {
        var p = await _toyRepo.GetByIdAsync(id);
        if (p is null) return;
        _toyRepo.Remove(p);
        await _toyRepo.SaveChangesAsync();
    }
}
