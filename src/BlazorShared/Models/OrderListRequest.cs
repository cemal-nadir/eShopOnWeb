using System;

namespace BlazorShared.Models;
public class OrderListRequest
{

    public string BuyerId { get; set; }
    public DateTimeOffset? OrderDateStart { get; set; }
    public DateTimeOffset? OrderDateEnd { get; set; }
    public decimal? OrderPriceTotalStart { get; set; }
    public decimal? OrderPriceTotalEnd { get; set; }
    public int? OrderItemCountMin { get; set; }
    public int? OrderItemCountMax { get; set; }
    public int? CatalogItemId { get; set; }
    public string CatalogItemName { get; set; }
}
