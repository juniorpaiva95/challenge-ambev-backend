using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale;

public class CancelSaleItemsRequest
{
    public List<Guid> ItemIds { get; set; } = new();
} 