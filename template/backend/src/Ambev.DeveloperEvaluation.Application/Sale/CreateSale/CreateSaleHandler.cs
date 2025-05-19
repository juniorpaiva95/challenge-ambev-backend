using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;
    private readonly IMediator _mediator;

    public CreateSaleHandler(ISaleRepository saleRepository, IProductRepository productRepository, IMapper mapper, ILogger<CreateSaleHandler> logger, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        // Mapear para entidade Sale
        var sale = _mapper.Map<Domain.Entities.Sale>(request);
        sale.SaleDate = request.SaleDate == default ? DateTime.UtcNow : request.SaleDate;
        sale.IsCancelled = false;

        // Buscar todos os produtos necessários
        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var products = new Dictionary<Guid, Domain.Entities.Product>();
        foreach (var productId in productIds)
        {
            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (product == null)
                throw new NotFoundException($"Produto {productId}");
            products[productId] = product;
        }

        sale.Items = request.Items.Select(item =>
        {
            var product = products[item.ProductId];
            var unitPrice = product.Price;
            var discount = DiscountCalculator.CalculateDiscount(item.Quantity);
            var total = item.Quantity * unitPrice * (1 - discount);
            return new SaleItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = unitPrice,
                Discount = discount,
                TotalAmount = total,
                IsCancelled = false
            };
        }).ToList();
        sale.TotalAmount = sale.Items.Sum(i => i.TotalAmount);

        // Validação de domínio
        var validation = sale.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors.Select(e =>
                new FluentValidation.Results.ValidationFailure(e.PropertyName, e.Detail)
            ));
        }

        // Salvar no repositório
        var created = await _saleRepository.CreateAsync(sale, cancellationToken);

        // Disparar evento e logar
        _logger.LogInformation("SaleCreatedEvent disparado para venda {SaleId}", created.Id);
        var saleCreatedEvent = new Ambev.DeveloperEvaluation.Domain.Events.SaleCreatedEvent(created);
        await _mediator.Publish(saleCreatedEvent, cancellationToken);

        // Mapear para resultado
        var result = _mapper.Map<CreateSaleResult>(created);
        result.Items = sale.Items.Select(saleItem => new SaleItemResult
        {
            ProductId = saleItem.ProductId,
            Quantity = saleItem.Quantity,
            UnitPrice = saleItem.UnitPrice,
            Discount = saleItem.Discount,
            TotalAmount = saleItem.TotalAmount,
            IsCancelled = saleItem.IsCancelled
        }).ToList();
        return result;
    }
} 