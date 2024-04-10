using System;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderListPagedRequest:BaseRequest
{
    public int PageSize { get; init; }
    public int PageIndex { get; init; }
    public string? BuyerId { get; init; }
    public DateTimeOffset?OrderDateStart { get; init; }
    public DateTimeOffset? OrderDateEnd { get; init; }
    public decimal? OrderPriceTotalStart { get; init; }
    public decimal? OrderPriceTotalEnd { get; init; }
    public int?OrderItemCountMin { get; init; }
    public int? OrderItemCountMax { get; init; }
    public int?CatalogItemId { get; init; }
    public string? CatalogItemName { get; init; }



    public OrderListPagedRequest(int? pageSize, int? pageIndex, string? buyerId, DateTimeOffset? orderDateStart,
        DateTimeOffset? orderDateEnd, decimal? orderPriceTotalStart, decimal? orderPriceTotalEnd,
        int? orderItemCountMin, int? orderItemCountMax, int? catalogItemId, string? catalogItemName)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        BuyerId = buyerId;
        OrderDateStart = orderDateStart;
        OrderDateEnd = orderDateEnd;
        OrderPriceTotalStart=orderPriceTotalStart;
        OrderPriceTotalEnd=orderPriceTotalEnd;
        OrderItemCountMin = orderItemCountMin;
        OrderItemCountMax=orderItemCountMax;
        CatalogItemId=catalogItemId;
        CatalogItemName=catalogItemName;
    }
}
