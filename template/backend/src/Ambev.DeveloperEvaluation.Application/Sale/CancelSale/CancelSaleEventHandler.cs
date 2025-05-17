using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale;

public class SaleCanceledEventHandler : INotificationHandler<SaleCancelledEvent>
{
    private readonly ILogger<SaleCanceledEventHandler> _logger;

    public SaleCanceledEventHandler(ILogger<SaleCanceledEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event SaleCanceledEvent received for sale {SaleId}", notification.Sale.Id);
        return Task.CompletedTask;
    }
} 