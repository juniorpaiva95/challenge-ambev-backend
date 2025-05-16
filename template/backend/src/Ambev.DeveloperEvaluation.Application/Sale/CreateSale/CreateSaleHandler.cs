using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        // Mapear para entidade Sale
        var sale = _mapper.Map<Domain.Entities.Sale>(request);
        sale.SaleDate = request.SaleDate == default ? DateTime.UtcNow : request.SaleDate;
        sale.IsCancelled = false;
        sale.Items = request.Items.Select(item =>
        {
            var discount = CalculateDiscount(item.Quantity);
            var total = item.Quantity * item.UnitPrice * (1 - discount);
            return new SaleItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = discount,
                TotalAmount = total,
                IsCancelled = false
            };
        }).ToList();
        sale.TotalAmount = sale.Items.Sum(i => i.TotalAmount);

        // Salvar no repositório
        var created = await _saleRepository.CreateAsync(sale, cancellationToken);

        // Mapear para resultado
        var result = _mapper.Map<CreateSaleResult>(created);
        result.Items = sale.Items.Select(i => new CreateSaleItemResultDto
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice,
            Discount = i.Discount,
            TotalAmount = i.TotalAmount,
            IsCancelled = i.IsCancelled
        }).ToList();
        return result;
    }

    private decimal CalculateDiscount(int quantity)
    {
        if (quantity < 4)
            return 0m;
        if (quantity >= 10 && quantity <= 20)
            return 0.20m;
        if (quantity >= 4)
            return 0.10m;
        return 0m;
    }
} 