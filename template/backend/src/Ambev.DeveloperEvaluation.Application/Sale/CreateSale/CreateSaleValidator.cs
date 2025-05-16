using FluentValidation;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(x => x.SaleNumber)
            .NotEmpty()
            .WithMessage("O número da venda é obrigatório.");
        RuleFor(x => x.SaleDate)
            .NotEmpty()
            .WithMessage("A data da venda é obrigatória.");
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("O cliente é obrigatório.");
        RuleFor(x => x.Branch)
            .NotEmpty()
            .WithMessage("A filial é obrigatória.");
        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("A venda deve conter pelo menos um item.");
        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemValidator());
    }
}

public class CreateSaleItemValidator : AbstractValidator<SaleItemCommand>
{
    public CreateSaleItemValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("O produto é obrigatório.");
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("A quantidade deve ser maior que zero.")
            .LessThanOrEqualTo(20)
            .WithMessage("A quantidade máxima por item é 20.");
        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage("O preço unitário deve ser maior que zero.");
    }
} 