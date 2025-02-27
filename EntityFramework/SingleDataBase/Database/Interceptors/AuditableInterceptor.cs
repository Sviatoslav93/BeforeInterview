using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SingleDataBase.Entities.Abstractions;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Database.Interceptors;

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
                entry.Entity.CreatedAt = timeProvider.GetUtcNow();
                entry.Entity.CreatedBy = currentUserProvider.UserId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
