using MediatR;
using Microsoft.AspNetCore.Mvc;
using SingleDataBase.Extensions;
using SingleDataBase.Features.Deals.CreateDeal;
using SingleDataBase.Features.Deals.ListDeals;

namespace SingleDataBase.Endpoints;

public static class DealsEndpoints
{
    public static void MapDealsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/deals");
        group.WithStoreCode();
        group.RequireAuthorization();

        group.MapGet("", async (IMediator mediator) =>
        {
            var deals = await mediator.Send(new ListDealsRequest());
            return Results.Ok(deals);
        });

        group.MapPost("", async (
            [FromBody] CreateDealRequest request,
            IMediator mediator) =>
        {
            var id = await mediator.Send(request);
            return Results.Created($"/api/deals/{id}", id);
        });
    }
}
