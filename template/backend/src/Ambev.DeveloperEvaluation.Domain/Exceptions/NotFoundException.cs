using System;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class NotFoundException : BusinessDomainException
{
    public NotFoundException(string resourceName)
        : base($"The resource {resourceName} was not found.")
    {
    }
} 