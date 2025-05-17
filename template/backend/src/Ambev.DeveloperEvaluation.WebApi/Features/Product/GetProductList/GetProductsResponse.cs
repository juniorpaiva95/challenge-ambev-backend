namespace Ambev.DeveloperEvaluation.WebApi.Features.GetProductList;

public class GetProductsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string SKU { get; set; } = string.Empty;
    public bool IsActive { get; set; }
} 