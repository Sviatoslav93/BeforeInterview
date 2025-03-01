using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Extensions;
using Result.Extensions;
using Store.Features.Deals.Create;
using Store.Features.Deals.Delete;
using Store.Features.Deals.List;

namespace Store.Endpoints;
public static class DealsEndpoints
{
    public static void MapDealsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/deals")
            .WithTags("Deals") // Add a tag for grouping in Swagger UI
            .WithOpenApi();

        group.WithStoreId();
        group.RequireAuthorization();

        // GET /api/deals endpoint
        group.MapGet("", (
            [AsParameters] ListDealsRequest listDealsRequest,
            IMediator mediator) =>
                mediator.Send(listDealsRequest)
                    .MatchAsync(
                        deal => Results.Ok(deal),
                        err => Results.Problem(err.First().Message, statusCode: StatusCodes.Status400BadRequest))
            )
            .WithName("GetDeals")
            .WithDescription("Retrieves all deals for the current store")
            .Produces<IEnumerable<DealView>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        // POST /api/deals endpoint
        group.MapPost("", (
            [FromBody] CreateDealRequest request,
            IMediator mediator) =>
                mediator.Send(request)
                    .MatchAsync(
                        id => Results.Created($"/api/deals/{id}", id),
                        err => Results.Problem(err.First().Message, statusCode: StatusCodes.Status400BadRequest))
            )
            .WithName("CreateDeal")
            .WithDescription("Creates a new deal for the current store")
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        // DELETE /api/deals/{id} endpoint
        group.MapDelete("{id}", (
            int id,
            IMediator mediator) =>
                mediator.Send(new DeleteDealRequest { Id = id })
                    .MatchAsync(
                        _ => Results.NoContent(),
                        err => Results.Problem(err.First().Message, statusCode: StatusCodes.Status400BadRequest))
            )
            .WithName("DeleteDeal")
            .WithDescription("Deletes a deal by ID")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
