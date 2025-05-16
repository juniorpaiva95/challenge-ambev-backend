using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

/// <summary>
/// Resultado da criação de uma venda.
/// </summary>
public class CreateSaleResult
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public string Branch { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public List<SaleItemResult> Items { get; set; } = new();
}

public class SaleItemResult
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
} 