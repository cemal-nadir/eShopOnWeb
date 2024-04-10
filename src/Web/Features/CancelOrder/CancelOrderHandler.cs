using MediatR;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.Features.MyOrders;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.CancelOrder;

public class CancelOrderHandler : IRequestHandler<CancelOrder>
{
    private readonly IRepository<Order> _orderRepository;

    public CancelOrderHandler(IRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderViewModel>> Handle(GetMyOrders request,
        CancellationToken cancellationToken)
    {
        var specification = new CustomerOrdersSpecification(request.UserName);
        var orders = await _orderRepository.ListAsync(specification, cancellationToken);

        return orders.Select(o => new OrderViewModel
        {
            OrderDate = o.OrderDate,
            OrderNumber = o.Id,
            ShippingAddress = o.ShipToAddress,
            Total = o.Total(),
            State = o.State
        });
    }

    public async Task Handle(Features.CancelOrder.CancelOrder request, CancellationToken cancellationToken)
    {
       var order=await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

       if (order is null) return;

       order.State.State = OrderStateType.Cancelled;

       await _orderRepository.UpdateAsync(order, cancellationToken);
    }
}
