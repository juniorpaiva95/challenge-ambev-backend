using MediatR;
using System;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sale;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public Guid SaleId { get; set; }
    public string? SaleNumber { get; set; }
    public DateTime? SaleDate { get; set; }
    public Guid? CustomerId { get; set; }
    public string? Branch { get; set; }
    public List<UpdateSaleItemDto>? Items { get; set; } = new();
}

public class UpdateSaleItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateSaleResult
{
    public Domain.Entities.Sale? Sale { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
} 