using MediatR;
using Result.Extensions;
using SingleDataBase.Features.Stores.Create;
using SingleDataBase.Features.Stores.ListStores;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Endpoints;

public static class StoresEndpoints
{
    public static void MapStoreEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/stores")
            .WithTags("Stores")
            .WithOpenApi();

        group.RequireAuthorization();

        // POST /api/stores endpoint
        group.MapPost("", (
            CreateStoreRequest request,
            IMediator mediator) =>
                mediator.Send(request)
                    .MatchAsync(
                        id => Results.Created($"/api/stores/{id}", id),
                        err => Results.Problem(err.First().Message, statusCode: StatusCodes.Status400BadRequest)
                    )
            )
            .WithName("CreateStore")
            .WithDescription("Creates a new store for the current user")
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        // GET /api/stores endpoint
        group.MapGet("", (
            IMediator mediator,
            ICurrentUserProvider currentUserProvider) =>
                mediator.Send(new ListStoresRequest { UserId = currentUserProvider.UserId })
                    .MatchAsync(
                        stores => Results.Ok(stores),
                        err => Results.Problem(err.First().Message, statusCode: StatusCodes.Status400BadRequest)
                    )
            )
            .WithName("GetStores")
            .WithDescription("Retrieves all stores for the current user")
            .Produces<IEnumerable<StoreView>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
