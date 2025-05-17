using System;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class SaleItemAlreadyCancelledException : BusinessDomainException
{
    public SaleItemAlreadyCancelledException(Guid itemId)
        : base($"Sale item '{itemId}' is already cancelled.")
    {
    }
} 