using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Result;
using SingleDataBase.Database;
using SingleDataBase.Entities;

namespace SingleDataBase.Features.Products.GetInfo;

public class GetProductHandler(StoreDbContext db) : IRequestHandler<GetProductRequest, Result<GetProductView>>
{
    public async Task<Result<GetProductView>> Handle(GetProductRequest request, CancellationToken cancellationToken)
    {
        var product = await db.Products
            .Where(p => p.Id == request.Id)
            .Select(MapGetProductView)
            .FirstOrDefaultAsync(cancellationToken);


        return product == null
            ? new Error("Product not found")
            : product;
    }

    public static Expression<Func<Product, GetProductView>> MapGetProductView => x
        => new GetProductView(
            x.Id,
            x.Name,
            x.ImageUrl,
            x.Description,
            x.Price,
            x.Quantity.Type,
            x.Quantity.Value
        );
}
