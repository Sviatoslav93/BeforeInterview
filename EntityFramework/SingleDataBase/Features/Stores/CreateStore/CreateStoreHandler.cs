using MediatR;
using SingleDataBase.Database;
using SingleDataBase.Entities;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Features.Stores.CreateStore;

public class CreateStoreHandler(StoreDbContext context, ICurrentUserProvider currentUserProvider) : IRequestHandler<CreateStoreRequest, Guid>
{
    private readonly StoreDbContext _context = context;

    public async Task<Guid> Handle(CreateStoreRequest request, CancellationToken cancellationToken)
    {
        var store = new Store
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            WebsiteUri = request.WebsiteUri,
            UserId = currentUserProvider.UserId
        };

        var storeEntry = _context.Stores.Add(store);
        await _context.SaveChangesAsync(cancellationToken);

        return storeEntry.Entity.Id;
    }
}
