using MediatR;
using Microsoft.EntityFrameworkCore;
using SingleDataBase.Database;

namespace SingleDataBase.Features.ListStores;

public class ListStoreHandler(StoreContext context) : IRequestHandler<ListStoresQuery, List<ListStoreView>>
{
    private readonly StoreContext _context = context;

    public async Task<List<ListStoreView>> Handle(ListStoresQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Stores
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return [.. entities.Select(e => new ListStoreView
        {
            Id = e.Id,
            Code = e.Code,
            Name = e.Name,
            WebsiteUri = e.WebsiteUri
        })];
    }
}
