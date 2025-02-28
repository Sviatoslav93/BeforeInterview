using MediatR;
using Microsoft.AspNetCore.Mvc;
using Result.Extensions;
using SingleDataBase.Extensions;
using SingleDataBase.Features.Products.Create;
using SingleDataBase.Features.Products.GetInfo;
using SingleDataBase.Features.Products.List;

namespace SingleDataBase.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products")
            .WithTags("Products")
            .WithOpenApi();

        group.WithStoreId();
        group.RequireAuthorization();

        // GET /api/products endpoint
        group.MapGet("", (
            [AsParameters] ListProductsRequest request,
            IMediator mediator) =>
                mediator.Send(request)
                    .MatchAsync(
                        products => Results.Ok(products),
                        err => Results.Problem(err.First().Message, statusCode: StatusCodes.Status400BadRequest)
                    )
            )
            .WithName("GetProducts")
            .WithDescription("Retrieves all products for the current store")
            .Produces<IEnumerable<ListProductView>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        // GET /api/products/{id} endpoint
        group.MapGet("{id}", (
            int id,
            IMediator mediator) =>
                mediator.Send(new GetProductRequest { Id = id })
                    .MatchAsync(
                        product => Results.Ok(product),
                        err => Results.Problem(err.First().Message, statusCode: StatusCodes.Status400BadRequest)
                    )
            )
            .WithName("GetProduct")
            .WithDescription("Retrieves a product by its ID")
            .Produces<GetProductView>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        // POST /api/products endpoint
        group.MapPost("", (
            [FromBody] CreateProductRequest request,
            IMediator mediator) =>
                mediator.Send(request)
                    .MatchAsync(
                        id => Results.Created($"/api/products/{id}", id),
                        err => Results.Problem(err.First().Message, statusCode: StatusCodes.Status400BadRequest)
                    )
            )
            .WithName("CreateProduct")
            .WithDescription("Creates a new product for the current store")
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
