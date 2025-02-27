using MediatR;
using SingleDataBase.Features.Stores.CreateStore;
using SingleDataBase.Features.Stores.ListStores;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Endpoints;

public static class StoresEndpoints
{
    public static void MapStoreEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/stores").RequireAuthorization();

        group.MapPost("", async (CreateStoreRequest request, IMediator mediator) =>
        {
            var id = await mediator.Send(request);
            return Results.Ok(id);
        });

        group.MapGet("", async (IMediator mediator, ICurrentUserProvider currentUserProvider) =>
        {
            var stores = await mediator.Send(new ListStoresRequest { UserId = currentUserProvider.UserId });
            return Results.Ok(stores);
        });
    }
}
