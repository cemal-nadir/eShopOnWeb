using MediatR;

namespace Microsoft.eShopWeb.Web.Features.CancelOrder;

public class CancelOrder : IRequest
{
    public int OrderId { get; set; }

    public CancelOrder(int orderId)
    {
        OrderId = orderId;
    }
}
