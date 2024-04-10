using System;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
public class OrderState
{
    public OrderStateType State { get; set; }
    public DateTimeOffset UpdateTime { get; set; }
}

public enum OrderStateType
{
    Pending = 0,
    Preparing = 1,
    Shipping = 2,
    Completed = 3,
    Cancelled = 4,
    Approved = 5
}
