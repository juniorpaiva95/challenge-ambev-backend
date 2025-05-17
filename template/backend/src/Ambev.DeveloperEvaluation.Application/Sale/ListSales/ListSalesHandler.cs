using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sale.ListSales;

public class ListSalesHandler : IRequestHandler<ListSalesQuery, ListSalesResult>
{
    private readonly ISaleRepository _saleRepository;

    public ListSalesHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<ListSalesResult> Handle(ListSalesQuery request, CancellationToken cancellationToken)
    {
        var (sales, totalCount) = await _saleRepository.GetPagedAsync(request.PageNumber, request.PageSize, request.CustomerId, request.ProductId, cancellationToken);
        var totalPages = (int)System.Math.Ceiling(totalCount / (double)request.PageSize);
        var salesDto = sales.Select(s => new SaleDto
        {
            Id = s.Id,
            SaleNumber = s.SaleNumber,
            SaleDate = s.SaleDate,
            TotalAmount = s.TotalAmount,
            Branch = s.Branch,
            IsCancelled = s.IsCancelled,
            Customer = new UserDto
            {
                Id = s.Customer.Id,
                Username = s.Customer.Username,
                Email = s.Customer.Email
            },
            Items = s.Items.Select(i => new SaleItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ProductName = i.Product?.Name ?? string.Empty,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = i.Discount,
                TotalAmount = i.TotalAmount,
                IsCancelled = i.IsCancelled
            }).ToList()
        }).ToList();

        return new ListSalesResult
        {
            CurrentPage = request.PageNumber,
            TotalPages = totalPages,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            Sales = salesDto
        };
    }
} 