using System;

namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class BusinessDomainException : Exception
{
    public BusinessDomainException(string message) : base(message) { }
    public BusinessDomainException(string message, Exception inner) : base(message, inner) { }
} 