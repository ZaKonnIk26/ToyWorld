using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToyWorld.Data;

namespace ToyWorld.Repositories;

// Базовая реализация универсального репозитория поверх EF Core
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ToyDbContext _toys;
    protected readonly DbSet<T> _toyBox;

    public Repository(ToyDbContext context)
    {
        _toys = context;
        _toyBox = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync() => await _toyBox.ToListAsync();

    public virtual async Task<T?> GetByIdAsync(int id) => await _toyBox.FindAsync(id);

    // Поиск по произвольному условию
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _toyBox.Where(predicate).ToListAsync();

    public async Task AddAsync(T entity) => await _toyBox.AddAsync(entity);
    public void Update(T entity) => _toyBox.Update(entity);
    public void Remove(T entity) => _toyBox.Remove(entity);

    public async Task<int> SaveChangesAsync() => await _toys.SaveChangesAsync();
}
