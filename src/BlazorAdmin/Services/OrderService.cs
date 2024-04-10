using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorShared.Interfaces;
using BlazorShared.Models;

namespace BlazorAdmin.Services;

public class OrderService(HttpService httpService) : IOrderService
{

    public async Task<(List<Orders>,int)> ListAsync(int pageSize,int pageIndex, OrderListRequest request, CancellationToken cancellationToken = default)
    {
        var query =$"pageSize={pageSize}&pageIndex={pageIndex}{request.GenerateUrlQueryString()}";

        var response = await httpService.HttpGet<OrdersResponse>($"orders?{query}", cancellationToken);

        return (response.Orders, response.PageCount);
    }

    public async Task<Order> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await httpService.HttpGet<OrderResponse>($"orders/{id}", cancellationToken);

        return response.Order;
    }

    public async Task UpdateOrderStateAsync(int id, OrderStateType orderStateType, CancellationToken cancellationToken = default)
    {
        await httpService.HttpPost<UpdateOrderStateResponse>($"orders/{id}/updateState?state={orderStateType}", null,
            cancellationToken: cancellationToken);
    }
}
