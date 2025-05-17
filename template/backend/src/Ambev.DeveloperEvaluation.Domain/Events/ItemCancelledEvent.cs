using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class ItemCancelledEvent : INotification
{
    public SaleItem Item { get; }
    public ItemCancelledEvent(SaleItem item)
    {
        Item = item;
    }
}
