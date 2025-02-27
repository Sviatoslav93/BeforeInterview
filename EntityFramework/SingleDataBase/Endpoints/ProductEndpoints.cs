using MediatR;
using Microsoft.AspNetCore.Mvc;
using SingleDataBase.Extensions;
using SingleDataBase.Features.Products.CreateProduct;
using SingleDataBase.Features.Products.ListProducts;

namespace SingleDataBase.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products");
        group.WithStoreCode();
        group.RequireAuthorization();

        group.MapGet("", async (
            [AsParameters] ListProductsRequest request,
            IMediator mediator) =>
        {
            var getProductsResult = await mediator.Send(request);

            return Results.Ok(getProductsResult);
        });

        group.MapPost("", async (
            [FromBody] CreateProductRequest request,
            IMediator mediator) =>
        {
            var createProductResult = await mediator.Send(request);

            return createProductResult.Match(
                id => Results.Created($"/api/products/{id}", id),
                ex => Results.BadRequest(ex.Message)
            );
        });
    }
}
