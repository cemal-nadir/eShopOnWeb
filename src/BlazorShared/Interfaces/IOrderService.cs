using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;
public interface IOrderService
{
    Task<(List<Orders>,int)> ListAsync(int pageSize,int pageIndex, OrderListRequest request, CancellationToken cancellationToken = default);
    Task<Order> GetAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateOrderStateAsync(int id, OrderStateType orderStateType, CancellationToken cancellationToken = default);
}
