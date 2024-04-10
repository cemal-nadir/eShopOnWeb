using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderListPagedResponse:BaseResponse
{
    public OrderListPagedResponse(Guid correlationId):base(correlationId)
    {
        
    }

    public OrderListPagedResponse()
    {
        
    }

    public List<OrdersDto> Orders { get; set; } = new();
    public int PageCount { get; set; }
}
