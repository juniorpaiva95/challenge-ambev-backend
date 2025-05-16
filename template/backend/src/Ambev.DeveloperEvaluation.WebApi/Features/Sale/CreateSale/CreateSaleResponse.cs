using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

/// <summary>
/// API response model for CreateSale operation
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// The unique identifier of the created sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The sale number identifier
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// The date when the sale occurred
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The ID of the customer making the purchase
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// The branch where the sale was made
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Whether the sale is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// The items included in this sale
    /// </summary>
    public List<SaleItem> Items { get; set; } = new();

    /// <summary>
    /// The created at date of the sale
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The updated at date of the sale
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
