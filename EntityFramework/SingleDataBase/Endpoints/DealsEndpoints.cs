using MediatR;
using SingleDataBase.Features.Deals.CreateDeal;

namespace SingleDataBase.Endpoints;

public static class DealsEndpoints
{
    public static void MapDealsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/deals", async (CreateDealRequest request, IMediator mediator) =>
        {
            var id = await mediator.Send(request);
            return Results.Ok(id);
        });
    }
}
