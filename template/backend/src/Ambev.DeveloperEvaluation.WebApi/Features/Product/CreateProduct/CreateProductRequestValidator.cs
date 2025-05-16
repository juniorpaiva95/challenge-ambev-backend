using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.CreateProduct;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do produto deve ter no máximo 100 caracteres.");
        RuleFor(x => x.Description)
            .MaximumLength(255).WithMessage("A descrição deve ter no máximo 255 caracteres.");
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");
        RuleFor(x => x.SKU)
            .NotEmpty().WithMessage("O SKU é obrigatório.")
            .MaximumLength(50).WithMessage("O SKU deve ter no máximo 50 caracteres.");
    }
} 