using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .LessThanOrEqualTo(20)
            .WithMessage("A quantidade deve estar entre 1 e 20 itens");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0);

        RuleFor(item => item.Discount)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(1);
    }
}