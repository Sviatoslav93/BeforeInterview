using MediatR;
using SingleDataBase.Features.Stores.CreateStore;
using SingleDataBase.Features.Stores.ListStores;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Endpoints;

public static class StoresEndpoints
{
    public static void MapStoreEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/stores", async (CreateStoreRequest request, IMediator mediator) =>
        {
            var id = await mediator.Send(request);
            return Results.Ok(id);
        });

        app.MapGet("api/stores", async (IMediator mediator, ICurrentUserProvider currentUserProvider) =>
        {
            var stores = await mediator.Send(new ListStoresRequest { UserId = currentUserProvider.GetUserId() });
            return Results.Ok(stores);
        });
    }
}
