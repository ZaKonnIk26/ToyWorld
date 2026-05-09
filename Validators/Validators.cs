using FluentValidation;
using ToyWorld.Models;

namespace ToyWorld.Validators;

// FluentValidation: проверка корректности модели Customer
public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c => c.FullName)
            .NotEmpty().WithMessage("ФИО обязательно")
            .MaximumLength(150);

        // Проверяем формат email
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email обязателен")
            .EmailAddress().WithMessage("Некорректный email")
            .MaximumLength(200);

        RuleFor(c => c.Phone).MaximumLength(30);
        RuleFor(c => c.Address).MaximumLength(500);
    }
}

// Проверка формы оформления заказа
public class CheckoutValidator : AbstractValidator<CheckoutModel>
{
    public CheckoutValidator()
    {
        RuleFor(c => c.FullName).NotEmpty().MaximumLength(150);
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.ShippingAddress).NotEmpty().MaximumLength(500);
    }
}

// Модель данных формы оформления заказа на странице Checkout
public class CheckoutModel
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
}
