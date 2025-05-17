using System;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class SaleItemNotFoundException : BusinessDomainException
{
    public SaleItemNotFoundException(Guid saleId, Guid itemId)
        : base($"Sale item '{itemId}' not found for sale '{saleId}'.")
    {
    }
} 