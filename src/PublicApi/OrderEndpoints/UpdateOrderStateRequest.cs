using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class UpdateOrderStateRequest:BaseRequest
{
    public int OrderId { get; set;}
    public OrderStateType State { get; set; }
}
