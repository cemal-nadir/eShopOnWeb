using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

/// <summary>
/// Updates a Catalog Item
/// </summary>
public class UpdateOrderStateEndpoint : IEndpoint<IResult, UpdateOrderStateRequest, IRepository<Order>>
{


    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/orders/{orderId}/updateState",
                [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS,
                    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async (int orderId, OrderStateType state, IRepository<Order> itemRepository) =>
                    await HandleAsync(new UpdateOrderStateRequest() { State = state, OrderId = orderId }, itemRepository))
            .Produces<UpdateOrderStateResponse>(204)
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateOrderStateRequest request, IRepository<Order> repository)
    {
        var response = new UpdateOrderStateResponse(request.CorrelationId());

        var existingItem = await repository.GetByIdAsync(request.OrderId);
        if (existingItem == null)
        {
            return Results.NotFound();
        }

        existingItem.State.State = request.State;
        existingItem.State.UpdateTime = DateTimeOffset.Now;

        await repository.UpdateAsync(existingItem);
        return Results.Ok(response);
    }
}
