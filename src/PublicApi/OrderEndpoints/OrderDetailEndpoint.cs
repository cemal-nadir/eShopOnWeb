using System.Security.Cryptography.X509Certificates;
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

public class OrderDetailEndpoint(IMapper mapper, IUriComposer uriComposer) : IEndpoint<IResult, GetByIdOrderRequest, IRepository<Order>>
{
    public async Task<IResult> HandleAsync(GetByIdOrderRequest request, IRepository<Order> repository)
    {
        var response = new GetByIdOrderResponse(request.CorrelationId());

        var item = await repository.FirstOrDefaultAsync(new OrderWithItemsByIdSpec(request.OrderId));
        if (item is null)
            return Results.NotFound();

        response.Order = mapper.Map<OrderDto>(item);

        response.Order.OrderItems?.ForEach(orderItem =>
        {
            orderItem.ItemOrdered.PictureUri = uriComposer.ComposePicUri(orderItem.ItemOrdered.PictureUri);
        });


        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders/{orderId}",
                async (int orderId, IRepository<Order> itemRepository) =>
                    await HandleAsync(new GetByIdOrderRequest(orderId), itemRepository))
            .Produces<GetByIdOrderResponse>()
            .WithTags("OrderEndpoints");
    }
}
