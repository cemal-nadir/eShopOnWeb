using System;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class UpdateOrderStateResponse:BaseResponse
{
    public UpdateOrderStateResponse(Guid correlationId) : base(correlationId)
    {

    }

    public UpdateOrderStateResponse()
    {

    }
}
