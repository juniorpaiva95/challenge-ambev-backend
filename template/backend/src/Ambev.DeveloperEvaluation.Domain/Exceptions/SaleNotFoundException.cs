using System;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class SaleNotFoundException : BusinessDomainException
{
    public SaleNotFoundException(Guid saleId)
        : base($"Sale with ID '{saleId}' was not found.")
    {
    }
} 