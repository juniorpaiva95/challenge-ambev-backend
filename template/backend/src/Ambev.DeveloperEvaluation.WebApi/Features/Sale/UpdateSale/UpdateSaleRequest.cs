using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale;

public class UpdateSaleRequest
{
    public string? SaleNumber { get; set; }
    public DateTime? SaleDate { get; set; }
    public Guid? CustomerId { get; set; }
    public string? Branch { get; set; }
    public List<UpdateSaleItemRequest>? Items { get; set; }
}

public class UpdateSaleItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
} 