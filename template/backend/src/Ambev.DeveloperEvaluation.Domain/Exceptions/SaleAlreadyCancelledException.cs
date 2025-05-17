using System;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class SaleAlreadyCancelledException : BusinessDomainException
{
    public SaleAlreadyCancelledException(Guid saleId)
        : base($"Sale with ID '{saleId}' is already cancelled.")
    {
    }
} 