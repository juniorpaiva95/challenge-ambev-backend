using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sale.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sale.ListSales;
using Ambev.DeveloperEvaluation.Application.Sale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale;

/// <summary>
/// Controller for managing sale operations
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class SaleController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SaleController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SaleController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<CreateSaleResponse>(response)
        });
    }

    [HttpPost("Cancel")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelSale([FromBody] Guid saleId, CancellationToken cancellationToken)
    {
        var command = new CancelSaleCommand(saleId);
        var result = await _mediator.Send(command, cancellationToken);
        if (!result)
            return NotFound($"Sale {saleId} not found.");
        return Ok(new ApiResponse { Success = true, Message = $"Sale {saleId} canceled successfully." });
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<SaleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListSales([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] Guid? customerId = null, [FromQuery] Guid? productId = null, CancellationToken cancellationToken = default)
    {
        var query = new ListSalesQuery(pageNumber, pageSize, customerId, productId);
        var result = await _mediator.Send(query, cancellationToken);
        var paginated = new PaginatedList<SaleDto>(result.Sales, result.TotalCount, result.CurrentPage, result.PageSize);

        return Ok(paginated);
    }

    [HttpPatch("{saleId}/CancelItem")]
    [ProducesResponseType(typeof(ApiResponseWithData<Ambev.DeveloperEvaluation.Application.Sale.CancelSale.CancelSaleItemsResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CancelSaleItems([FromRoute] Guid saleId, [FromBody] CancelSaleItemsRequest request, CancellationToken cancellationToken)
    {
        var command = new Ambev.DeveloperEvaluation.Application.Sale.CancelSale.CancelSaleItemsCommand(saleId, request.ItemIds);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResponseWithData<Ambev.DeveloperEvaluation.Application.Sale.CancelSale.CancelSaleItemsResult>
        {
            Success = true,
            Message = "Batch cancel operation completed.",
            Data = result
        });
    }

    [HttpPatch("{saleId}/Update")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid saleId, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateSaleCommand>(request);
        command.SaleId = saleId;
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.Success)
            return BadRequest(new ApiResponse { Success = false, Message = result.Message });
        return Ok<UpdateSaleResult>(result);
    }
}
