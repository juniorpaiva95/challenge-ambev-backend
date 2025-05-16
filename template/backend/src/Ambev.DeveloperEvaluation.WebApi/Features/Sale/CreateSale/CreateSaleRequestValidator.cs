using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleNumber: Required, not empty
    /// - SaleDate: Required, must be valid date
    /// - CustomerId: Required, must be valid GUID
    /// - TotalAmount: Must be greater than zero
    /// - Branch: Required, not empty
    /// - Items: Must have at least one item
    /// </remarks>
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.SaleNumber).NotEmpty();
        RuleFor(sale => sale.SaleDate).NotEmpty();
        RuleFor(sale => sale.CustomerId).NotEmpty();
        RuleFor(sale => sale.Branch).NotEmpty();
        RuleFor(sale => sale.Items).NotEmpty().WithMessage("At least one item is required");
    }
}
