using MediatR;
using Microsoft.EntityFrameworkCore;
using SingleDataBase.Database;

namespace SingleDataBase.Features.Stores.ListStores;

public class ListStoreHandler(StoreDbContext context) : IRequestHandler<ListStoresRequest, List<StoreView>>
{
    public Task<List<StoreView>> Handle(ListStoresRequest request, CancellationToken cancellationToken)
    {
        return context.Stores
            .Where(s => s.UserId == request.UserId)
            .Select(s => new StoreView
            {
                StoreCode = s.StoreCode,
                Name = s.Name,
                WebsiteUri = s.WebsiteUri
            })
            .ToListAsync(cancellationToken);
    }
}
