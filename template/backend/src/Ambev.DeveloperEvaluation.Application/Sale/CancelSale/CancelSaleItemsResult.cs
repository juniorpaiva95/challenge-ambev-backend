using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale;

public class CancelSaleItemsResult
{
    public List<CancelSaleItemResult> Results { get; set; } = new();
}

public class CancelSaleItemResult
{
    public Guid ItemId { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
} 