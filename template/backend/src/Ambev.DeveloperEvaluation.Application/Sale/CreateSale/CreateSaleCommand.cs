using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

/// <summary>
/// Command para criar uma nova venda.
/// </summary>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public string Branch { get; set; } = string.Empty;
    public List<SaleItemCommand> Items { get; set; } = new();
}

public class SaleItemCommand
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
} 