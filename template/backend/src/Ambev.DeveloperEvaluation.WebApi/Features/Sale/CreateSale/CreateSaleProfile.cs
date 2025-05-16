using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

/// <summary>
/// Profile for mapping between Application and API CreateSale responses
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale feature
    /// </summary>
    public CreateSaleProfile()
    {
        // Request -> Command
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<SaleItemRequest, SaleItemCommand>();

        // Result -> Response
        CreateMap<CreateSaleResult, CreateSaleResponse>();
        CreateMap<SaleItemResult, SaleItemResponse>();
    }
}
