using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderListPagedEndpoint:IEndpoint<IResult,OrderListPagedRequest,IRepository<Order>>
{
    private readonly IMapper _mapper;
    public OrderListPagedEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<IResult> HandleAsync(OrderListPagedRequest request, IRepository<Order> repository)
    {
        var response = new OrderListPagedResponse(request.CorrelationId());

        var filterSpec = new OrderFilterPaginatedSpecification(request.BuyerId, request.OrderDateStart, request.OrderDateEnd,
            request.OrderPriceTotalStart, request.OrderPriceTotalEnd, request.OrderItemCountMin,
            request.OrderItemCountMax, request.CatalogItemId, request.CatalogItemName);

        int totalItems = await repository.CountAsync(filterSpec);

        var pagedSpec = new OrderFilterPaginatedSpecification(skip: request.PageIndex * request.PageSize,
            take: request.PageSize, request.BuyerId, request.OrderDateStart, request.OrderDateEnd,
            request.OrderPriceTotalStart, request.OrderPriceTotalEnd, request.OrderItemCountMin,
            request.OrderItemCountMax, request.CatalogItemId, request.CatalogItemName);

        var items = await repository.ListAsync(pagedSpec);

        response.Orders.AddRange(items.Select(_mapper.Map<OrdersDto>));
     

        if (request.PageSize > 0)
        {
            response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize).ToString());
        }
        else
        {
            response.PageCount = totalItems > 0 ? 1 : 0;
        }

        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders",
                async (int? pageSize, int? pageIndex, string? buyerId, DateTimeOffset? orderDateStart,
                    DateTimeOffset? orderDateEnd, decimal? orderPriceTotalStart, decimal? orderPriceTotalEnd,
                    int? orderItemCountMin, int? orderItemCountMax, int? catalogItemId, string? catalogItemName,
                    IRepository<Order> repository) => await HandleAsync(
                    new OrderListPagedRequest(pageSize, pageIndex, buyerId, orderDateStart, orderDateEnd,
                        orderPriceTotalStart, orderPriceTotalEnd, orderItemCountMin, orderItemCountMax, catalogItemId,
                        catalogItemName), repository))
            .Produces<OrderListPagedResponse>()
            .WithTags("OrderEndpoints");
    }
}
