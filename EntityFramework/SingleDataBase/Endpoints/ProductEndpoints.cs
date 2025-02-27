using MediatR;
using Microsoft.AspNetCore.Mvc;
using SingleDataBase.Extensions;
using SingleDataBase.Features.Products.CreateProduct;
using SingleDataBase.Features.Products.ListProducts;
using Microsoft.AspNetCore.Http;

namespace SingleDataBase.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products")
            .WithTags("Products")
            .WithOpenApi();

        group.WithStoreCode();
        group.RequireAuthorization();

        group.MapGet("", async (
            [AsParameters] ListProductsRequest request,
            IMediator mediator) =>
        {
            var getProductsResult = await mediator.Send(request);

            return Results.Ok(getProductsResult);
        })
        .WithName("GetProducts")
        .WithDescription("Retrieves all products for the current store")
        .Produces<IEnumerable<ProductView>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapPost("", async (
            [FromBody] CreateProductRequest request,
            IMediator mediator) =>
        {
            var createProductResult = await mediator.Send(request);

            return createProductResult.Match(
                id => Results.Created($"/api/products/{id}", id),
                ex => Results.BadRequest(ex.Message)
            );
        })
        .WithName("CreateProduct")
        .WithDescription("Creates a new product for the current store")
        .Produces<int>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
