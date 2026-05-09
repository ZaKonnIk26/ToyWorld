using Microsoft.EntityFrameworkCore;
using ToyWorld.Data;
using ToyWorld.Models;

namespace ToyWorld.Services;

// Упрощённый сервис текущего пользователя (без авторизации, для демо)
public interface ICurrentUserService
{
    Task<Customer> GetOrCreateGuestKidAsync();
}

public class CurrentUserService : ICurrentUserService
{
    private readonly ToyDbContext _toys;

    // Email фиктивного покупателя демо-режима
    private const string GuestKidEmail = "guest@toyworld.local";

    public CurrentUserService(ToyDbContext context) => _toys = context;

    // Возвращает существующего демо-покупателя или создаёт нового
    public async Task<Customer> GetOrCreateGuestKidAsync()
    {
        var customer = await _toys.Customers.FirstOrDefaultAsync(c => c.Email == GuestKidEmail);

        if (customer == null)
        {
            customer = new Customer
            {
                FullName = "Demo Kid",
                Email = GuestKidEmail,
                RegisteredAt = DateTime.UtcNow
            };
            _toys.Customers.Add(customer);
            await _toys.SaveChangesAsync();
        }
        return customer;
    }
}
