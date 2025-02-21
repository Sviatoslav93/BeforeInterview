using MediatR;
using SingleDataBase.Database;
using SingleDataBase.Entities;

namespace SingleDataBase.Features.AddStore;

public class AddStoreHandler(StoreContext context) : IRequestHandler<AddStoreCommand, int>
{
    private readonly StoreContext _context = context;

    public async Task<int> Handle(AddStoreCommand request, CancellationToken cancellationToken)
    {
        var store = new Store
        {
            Code = request.Code,
            Name = request.Name,
            WebsiteUri = request.WebsiteUri ?? "example.com"
        };

        var entry = _context.Stores.Add(store);
        await _context.SaveChangesAsync(cancellationToken);

        return entry.Entity.Id;
    }
}
