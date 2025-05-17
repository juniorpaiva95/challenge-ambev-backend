using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale;

public class CancelSaleItemsHandler : IRequestHandler<CancelSaleItemsCommand, CancelSaleItemsResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<CancelSaleItemsHandler> _logger;

    public CancelSaleItemsHandler(ISaleRepository saleRepository, IMediator mediator, ILogger<CancelSaleItemsHandler> logger)
    {
        _saleRepository = saleRepository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<CancelSaleItemsResult> Handle(CancelSaleItemsCommand request, CancellationToken cancellationToken)
    {
        var results = new List<CancelSaleItemResult>();
        foreach (var itemId in request.ItemIds.Distinct())
        {
            var item = await _saleRepository.GetItemByIdAsync(itemId, cancellationToken);
            if (item == null || item.SaleId != request.SaleId)
            {
                results.Add(new CancelSaleItemResult { ItemId = itemId, Success = false, Message = "Item not found for this sale." });
                continue;
            }
            if (item.IsCancelled)
            {
                results.Add(new CancelSaleItemResult { ItemId = itemId, Success = false, Message = "Item already cancelled." });
                continue;
            }
            item.IsCancelled = true;
            var success = await _saleRepository.UpdateItemAsync(item, cancellationToken);
            if (success)
            {
                await _mediator.Publish(new ItemCancelledEvent(item), cancellationToken);
                results.Add(new CancelSaleItemResult { ItemId = itemId, Success = true, Message = "Item cancelled successfully." });
            }
            else
            {
                results.Add(new CancelSaleItemResult { ItemId = itemId, Success = false, Message = "Failed to cancel item." });
            }
        }
        return new CancelSaleItemsResult { Results = results };
    }
} 