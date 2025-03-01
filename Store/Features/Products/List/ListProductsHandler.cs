using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Result;
using Result.Extensions;
using Store.Database;
using Store.Entities;
using Store.Utils;

namespace Store.Features.Products.List;

public class ListProductsHandler(StoreDbContext db) : IRequestHandler<ListProductsRequest, Result<List<ListProductView>>>
{
    public async Task<Result<List<ListProductView>>> Handle(ListProductsRequest request, CancellationToken cancellationToken)
    {
        var count = await db.Products.CountAsync(cancellationToken);

        return await PaginationHelper.GetFromTo(request.PageNumber, request.PageSize, count)
            .ThenAsync(async p =>
            {
                return await db.Products
                    .OrderBy(x => x.Name)
                    .Skip(p.From)
                    .Take(p.To - p.From)
                    .Select(MapListProductView)
                    .ToListAsync(cancellationToken);
            });
    }

    private static Expression<Func<Product, ListProductView>> MapListProductView => x
        => new ListProductView(
            x.Id,
            x.StoreId,
            x.Name,
            x.ImageUrl,
            x.Price,
            x.Quantity.Type,
            x.Quantity.Value
        );
}
