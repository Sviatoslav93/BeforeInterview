using MediatR;
using Result;
using Store.Database;
using Store.Entities;

namespace Store.Features.Products.Create;

public class CreateProductHandler(StoreDbContext db) : IRequestHandler<CreateProductRequest, Result<int>>
{
    public async Task<Result<int>> Handle(
        CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = Product.Create(
            name: request.Name,
            price: request.Price,
            quantity: new Quantity(request.Quantity, request.QuantityType),
            imageUrl: request.ImageUrl,
            description: request.Description);

        db.Products.Add(product);
        await db.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
