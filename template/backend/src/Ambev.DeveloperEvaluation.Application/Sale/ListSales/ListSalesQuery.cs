using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sale.ListSales;

public record ListSalesQuery(int PageNumber = 1, int PageSize = 10, Guid? CustomerId = null, Guid? ProductId = null) : IRequest<ListSalesResult>; 