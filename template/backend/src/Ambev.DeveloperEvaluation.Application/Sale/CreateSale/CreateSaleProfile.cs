using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

/// <summary>
/// Profile para mapeamento entre entidades e DTOs de venda.
/// </summary>
public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        // Command -> Entity
        CreateMap<CreateSaleCommand, Domain.Entities.Sale>();
        CreateMap<SaleItemCommand, SaleItem>();

        // Entity -> Result
        CreateMap<Domain.Entities.Sale, CreateSaleResult>();
        CreateMap<SaleItem, SaleItemResult>();
    }
} 