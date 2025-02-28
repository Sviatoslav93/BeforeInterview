using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using SingleDataBase.Database;
using SingleDataBase.Extensions;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Middlewares;

public class CheckIdCodeMiddleware(
    RequestDelegate next,
    HybridCache cache)
{
    private readonly RequestDelegate _next = next;
    private readonly HybridCache _cache = cache;

    public async Task InvokeAsync(HttpContext context)
    {
        var dbContext = context.RequestServices.GetRequiredService<StoreDbContext>();
        var storeIdProvider = context.RequestServices.GetRequiredService<IStoreIdProvider>();
        var currentUserProvider = context.RequestServices.GetRequiredService<ICurrentUserProvider>();

        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata.GetMetadata<StoreIdMetadata>() != null)
        {
            var storeId = storeIdProvider.StoreId;

            var userId = currentUserProvider.UserId;

            var availableStores = await AvailableStores(userId, dbContext);
            if (!availableStores.Contains(storeId))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
        }

        await _next(context);
    }

    public async Task<IEnumerable<Guid>> AvailableStores(
        Guid userId,
        StoreDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        var availableStores = await _cache.GetOrCreateAsync($"AvailableStores:{userId}", async ct =>
        {
            return await dbContext.Stores
                .Where(us => us.UserId == userId)
                .Select(us => us.Id)
                .ToListAsync(cancellationToken: ct);
        }, cancellationToken: cancellationToken);

        return availableStores is null ? Enumerable.Empty<Guid>() : availableStores;
    }
}
