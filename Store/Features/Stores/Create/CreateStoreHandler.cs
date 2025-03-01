using MediatR;
using Result;
using Result.Extensions;
using Store.Entities;
using Store.Database;
using Store.Features.Stores.Events;
using Store.Services.Abstractions;

namespace Store.Features.Stores.Create;

public class CreateStoreHandler(
    StoreDbContext context,
    ICurrentUserProvider currentUserProvider) : IRequestHandler<CreateStoreRequest, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateStoreRequest request, CancellationToken cancellationToken)
    {
        return await Entities.Store.Create(
            name: request.Name,
            userId: currentUserProvider.UserId,
            websiteUri: request.WebsiteUri)
            .ThenAsync(
                async store =>
                {
                    store.AddDomainEvents(new StoreCreatedEvent(store.Id, currentUserProvider.UserId));

                    var storeEntry = context.Stores.Add(store);
                    await context.SaveChangesAsync(cancellationToken);

                    return storeEntry.Entity.Id;
                }
            );
    }
}
