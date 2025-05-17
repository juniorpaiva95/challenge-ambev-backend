using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale;

public record CancelSaleItemsCommand(Guid SaleId, List<Guid> ItemIds) : IRequest<CancelSaleItemsResult>; 