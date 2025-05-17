using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sale.ListSales;

public class ListSalesResult
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<SaleDto> Sales { get; set; } = new();
}

public class SaleDto
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Branch { get; set; }
    public bool IsCancelled { get; set; }
    public UserDto Customer { get; set; }
    public List<SaleItemDto> Items { get; set; } = new();
}

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}

public class SaleItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
} 