using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProductList;

public class GetProductsProfile : Profile
{
    public GetProductsProfile()
    {
        CreateMap<Domain.Entities.Product, GetProductsResult>();
    }
} 