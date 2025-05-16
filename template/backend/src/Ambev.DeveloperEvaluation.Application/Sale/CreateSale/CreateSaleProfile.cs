using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

/// <summary>
/// Profile para mapeamento entre entidades e DTOs de venda.
/// </summary>
public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Domain.Entities.Sale>();
        CreateMap<Domain.Entities.Sale, CreateSaleResult>();
        CreateMap<CreateSaleItemDto, Domain.Entities.SaleItem>();
        CreateMap<Domain.Entities.SaleItem, CreateSaleItemResultDto>();
    }
} 