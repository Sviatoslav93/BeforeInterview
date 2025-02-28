using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Result;
using SingleDataBase.Database;
using SingleDataBase.Entities;

namespace SingleDataBase.Features.Stores.ListStores;

public class ListStoreHandler(StoreDbContext context) : IRequestHandler<ListStoresRequest, Result<List<StoreView>>>
{
    public async Task<Result<List<StoreView>>> Handle(ListStoresRequest request, CancellationToken cancellationToken)
    {
        return await context.Stores
            .Select(ToStoreView)
            .ToListAsync(cancellationToken);
    }

    public static Expression<Func<Store, StoreView>> ToStoreView => s
    => new StoreView(
        s.Id,
        s.Name,
        s.WebsiteUri
    );
}
