using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SingleDataBase.Entities.Abstractions;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Database.Interceptors;

public class StoreIdInterseptor(IStoreIdProvider storeIdProvider) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context!;
        var entries = context.ChangeTracker.Entries<IStoreId>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.StoreId = storeIdProvider.StoreId;
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
