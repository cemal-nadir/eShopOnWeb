using System;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrdersDto
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public OrderStateDto State { get; set; }
    public decimal Total { get; set; }
}
