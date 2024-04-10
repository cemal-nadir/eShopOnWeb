using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderDto
{
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public OrderStateDto State { get; set; }
    public AddressDto ShipToAddress { get; set; }
    public List<OrderItemDto>? OrderItems { get; set; }
    public decimal Total { get; set; }
}
