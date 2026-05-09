using ToyWorld.Models;
using ToyWorld.Repositories;

namespace ToyWorld.Services;

// Бизнес-сервис для работы с категориями товаров
public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task CreateAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
}

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _toyRepo;

    public CategoryService(IRepository<Category> repo) => _toyRepo = repo;

    public Task<IEnumerable<Category>> GetAllAsync() => _toyRepo.GetAllAsync();
    public Task<Category?> GetByIdAsync(int id) => _toyRepo.GetByIdAsync(id);

    public async Task CreateAsync(Category category)
    {
        await _toyRepo.AddAsync(category);
        await _toyRepo.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _toyRepo.Update(category);
        await _toyRepo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var c = await _toyRepo.GetByIdAsync(id);
        if (c is null) return;
        _toyRepo.Remove(c);
        await _toyRepo.SaveChangesAsync();
    }
}
