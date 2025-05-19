using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale;
using Ambev.DeveloperEvaluation.Application.Sale;
using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
        CreateMap<UpdateSaleItemRequest, UpdateSaleItemDto>();
    }
} 