using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, bool>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ILogger<CancelSaleHandler> _logger;
    private readonly IMediator _mediator;

    public CancelSaleHandler(ISaleRepository saleRepository, ILogger<CancelSaleHandler> logger, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
        if (sale == null)
        {
            throw new SaleNotFoundException(request.SaleId);
        }
        if (sale.IsCancelled)
        {
            throw new SaleAlreadyCancelledException(request.SaleId);
        }
        sale.IsCancelled = true;
        sale.UpdatedAt = DateTime.UtcNow;
        var result = await _saleRepository.UpdateAsync(sale, cancellationToken);
        _logger.LogInformation("Sale {SaleId} canceled successfully.", request.SaleId);

        // Dispatch event to notify other services
        var saleCanceledEvent = new SaleCancelledEvent(sale);
        await _mediator.Publish(saleCanceledEvent, cancellationToken);

        return result;
    }
} 