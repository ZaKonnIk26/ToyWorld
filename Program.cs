using Blazored.Modal;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ToyWorld.Components;
using ToyWorld.Data;
using ToyWorld.Repositories;
using ToyWorld.Services;
using ToyWorld.Validators;

var builder = WebApplication.CreateBuilder(args);

// Blazor Server с интерактивными компонентами
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// PostgreSQL через Entity Framework Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ToyDbContext>(options =>
    options.UseNpgsql(connectionString));

// Репозитории и сервисы бизнес-логики (один экземпляр на запрос)
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Все валидаторы FluentValidation из сборки + модальные окна
builder.Services.AddValidatorsFromAssemblyContaining<CheckoutValidator>();
builder.Services.AddBlazoredModal();

var app = builder.Build();

// Автоматическое применение миграций при старте — удобно для Docker
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ToyDbContext>();
    try
    {
        db.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка применения миграций");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Точка входа Blazor-приложения
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
