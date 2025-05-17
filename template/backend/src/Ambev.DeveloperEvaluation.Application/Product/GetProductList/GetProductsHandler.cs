using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProductList;

public class GetProductsHandler : IRequestHandler<GetAllProductsQuery, PaginatedList<GetProductsResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetProductsResult>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var queryable = await _productRepository.GetQueryableAsync();
        var paged = await PaginatedList<Domain.Entities.Product>.CreateAsync(queryable, request.PageNumber, request.PageSize);
        var mapped = paged.Select(p => _mapper.Map<GetProductsResult>(p)).ToList();
        return new PaginatedList<GetProductsResult>(mapped, paged.TotalCount, paged.CurrentPage, paged.PageSize);
    }
} 