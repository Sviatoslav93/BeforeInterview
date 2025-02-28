using MediatR;
using Microsoft.AspNetCore.Mvc;
using SingleDataBase.Extensions;
using SingleDataBase.Features.Deals.CreateDeal;
using SingleDataBase.Features.Deals.ListDeals;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace SingleDataBase.Endpoints;

public static class DealsEndpoints
{
    public static void MapDealsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/deals")
            .WithTags("Deals") // Add a tag for grouping in Swagger UI
            .WithOpenApi();

        group.WithStoreId();
        group.RequireAuthorization();

        group.MapGet("", async (
            [AsParameters] ListDealsRequest listDealsRequest,
            IMediator mediator) =>
        {
            var deals = await mediator.Send(listDealsRequest);
            return Results.Ok(deals);
        })
        .WithName("GetDeals")
        .WithDescription("Retrieves all deals for the current store")
        .Produces<IEnumerable<DealView>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPost("", async (
            [FromBody] CreateDealRequest request,
            IMediator mediator) =>
        {
            var id = await mediator.Send(request);
            return Results.Created($"/api/deals/{id}", id);
        })
        .WithName("CreateDeal")
        .WithDescription("Creates a new deal for the current store")
        .Produces<int>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
