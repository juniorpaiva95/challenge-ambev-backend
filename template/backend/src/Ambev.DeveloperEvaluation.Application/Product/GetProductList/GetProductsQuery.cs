using MediatR;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProductList;

public record GetAllProductsQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<GetProductsResult>>; 