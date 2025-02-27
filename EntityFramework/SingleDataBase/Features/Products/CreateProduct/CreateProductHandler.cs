using LanguageExt.Common;
using MediatR;
using SingleDataBase.Database;
using SingleDataBase.Entities;

namespace SingleDataBase.Features.Products.CreateProduct;

public class CreateProductHandler(StoreDbContext db) : IRequestHandler<CreateProductRequest, Result<int>>
{
    public async Task<Result<int>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            ImageUrl = request.ImageUrl,
            Description = request.Description,
            Price = request.Price,
            Quantity = new Quantity
            {
                Value = request.Quantity,
                Type = request.QuantityType
            }
        };

        db.Products.Add(product);
        await db.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
