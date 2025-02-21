using MediatR;
using Microsoft.AspNetCore.Mvc;
using SingleDataBase.Features.AddStore;
using SingleDataBase.Features.ListStores;

namespace SingleDataBase.Endpoints;

public static class StoreEndpoints
{
    public static void MapStoreEndpoints(this WebApplication app)
    {
        app.MapGet("api/stores", async ([AsParameters] ListStoresQuery request, [FromServices] IMediator mediator) =>
            {
                var stores = await mediator.Send(request);
                return Results.Ok(stores);
            })
            .WithName("ListStores");

        app.MapPost("api/stores", async (AddStoreCommand request, [FromServices] IMediator mediator) =>
            {
                var id = await mediator.Send(request);
                return Results.Created($"/stores/{id}", id);
            })
            .WithName("CreateStore");
    }
}