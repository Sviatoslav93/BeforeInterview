using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Store.Entities.Abstractions;
using Store.Services.Abstractions;

namespace Store.Database.Interceptors;

public class AuditableInterceptor(
    ICurrentUserProvider currentUserProvider,
    TimeProvider timeProvider) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context!;
        var entries = context.ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreateInfo(timeProvider.GetUtcNow(), currentUserProvider.UserId);
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetUpdateInfo(timeProvider.GetUtcNow(), currentUserProvider.UserId);
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
