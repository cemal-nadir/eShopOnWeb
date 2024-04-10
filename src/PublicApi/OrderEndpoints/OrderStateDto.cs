using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using System;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderStateDto
{
    public OrderStateType State { get; private set; }
    public DateTimeOffset UpdateTime { get; set; }
}
