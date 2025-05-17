using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale;

public record CancelSaleCommand(Guid SaleId) : IRequest<bool>; 