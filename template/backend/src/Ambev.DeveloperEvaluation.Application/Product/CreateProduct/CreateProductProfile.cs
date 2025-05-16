using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Product.CreateProduct;

/// <summary>
/// Profile para mapeamento entre entidades e DTOs de produto.
/// </summary>
public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Domain.Entities.Product>();
        CreateMap<Domain.Entities.Product, CreateProductResult>();
    }
} 