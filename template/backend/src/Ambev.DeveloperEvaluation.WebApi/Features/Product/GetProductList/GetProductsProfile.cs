using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Product.GetProductList;

namespace Ambev.DeveloperEvaluation.WebApi.Features.GetProductList;

public class GetProductsProfile : Profile
{
    public GetProductsProfile()
    {
        CreateMap<GetProductsResult, GetProductsResponse>();
    }
} 