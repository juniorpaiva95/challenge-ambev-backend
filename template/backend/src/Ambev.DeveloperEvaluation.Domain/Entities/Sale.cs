using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public User Customer { get; set; }
    public decimal TotalAmount { get; set; }
    public string Branch { get; set; } = string.Empty;
    public bool IsCancelled { get; set; }
    public List<SaleItem> Items { get; set; } = new List<SaleItem>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}