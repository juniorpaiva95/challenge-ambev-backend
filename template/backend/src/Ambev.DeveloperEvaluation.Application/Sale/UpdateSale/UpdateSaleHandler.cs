using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Sale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;

    public UpdateSaleHandler(ISaleRepository saleRepository, IUserRepository userRepository, IProductRepository productRepository)
    {
        _saleRepository = saleRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);
        if (sale == null)
            throw new NotFoundException($"Sale {command.SaleId}");

        // Atualiza campos da venda se vierem no payload
        if (command.SaleNumber != null) sale.SaleNumber = command.SaleNumber;
        if (command.SaleDate.HasValue) sale.SaleDate = command.SaleDate.Value;
        if (command.CustomerId.HasValue) sale.CustomerId = command.CustomerId.Value;
        if (command.Branch != null) sale.Branch = command.Branch;
        sale.UpdatedAt = DateTime.UtcNow;

        // Lógica de diff dos itens
        var payloadItems = command.Items ?? new List<UpdateSaleItemDto>();
        var currentItems = sale.Items ?? new List<SaleItem>();

        // Atualizar e adicionar
        foreach (var itemDto in payloadItems)
        {
            var product = await _productRepository.GetByIdAsync(itemDto.ProductId, cancellationToken);
            if (product == null)
                throw new BusinessDomainException($"Produto {itemDto.ProductId} não encontrado.");

            var existing = currentItems.FirstOrDefault(i => i.ProductId == itemDto.ProductId);
            var discount = DiscountCalculator.CalculateDiscount(itemDto.Quantity);
            if (existing != null)
            {
                existing.Quantity = itemDto.Quantity;
                existing.UnitPrice = product.Price;
                existing.Discount = discount;
                existing.TotalAmount = itemDto.Quantity * product.Price * (1 - discount);
            }
            else
            {
                currentItems.Add(new SaleItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price,
                    Discount = discount,
                    TotalAmount = itemDto.Quantity * product.Price * (1 - discount),
                    IsCancelled = false
                });
            }
        }

        // Remover itens que não estão mais no payload
        var payloadProductIds = payloadItems.Select(i => i.ProductId).ToHashSet();
        sale.Items = currentItems.Where(i => payloadProductIds.Contains(i.ProductId)).ToList();

        // Recalcular total
        sale.TotalAmount = sale.Items.Where(i => !i.IsCancelled).Sum(i => (i.UnitPrice - i.Discount) * i.Quantity);

        // Validação (reaproveitamento lógica de criação)
        var validation = sale.Validate();
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors.Select(e =>
                new FluentValidation.Results.ValidationFailure(e.PropertyName, e.Detail)
            ));
        }

        await _saleRepository.UpdateAsync(sale, cancellationToken);
        return new UpdateSaleResult
        {
            Success = true,
            Sale = sale,
            Message = "Sale updated successfully"
        };
    }

} 