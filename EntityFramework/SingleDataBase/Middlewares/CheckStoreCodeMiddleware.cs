using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using SingleDataBase.Database;
using SingleDataBase.Extensions;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Middlewares;

public class CheckStoreCodeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HybridCache _cache;
    public CheckStoreCodeMiddleware(RequestDelegate next, HybridCache cache)
    {
        _next = next;
        _cache = cache;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var dbContext = context.RequestServices.GetRequiredService<StoreDbContext>();
        var storeCodeProvider = context.RequestServices.GetRequiredService<IStoreCodeProvider>();
        var currentUserProvider = context.RequestServices.GetRequiredService<ICurrentUserProvider>();

        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata.GetMetadata<StoreCodeMetadata>() != null)
        {
            var storeCode = storeCodeProvider.StoreCode;

            var userId = currentUserProvider.UserId;

            var availableStores = await AvailableStores(userId, dbContext);
            if (!availableStores.Contains(storeCode))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
        }

        await _next(context);
    }

    public async Task<IEnumerable<Guid>> AvailableStores(Guid userId, StoreDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var availableStores = await _cache.GetOrCreateAsync($"AvailableStores:{userId}", async ct =>
        {
            return await dbContext.Stores
                .Where(us => us.UserId == userId)
                .Select(us => us.StoreCode)
                .ToListAsync(cancellationToken: ct);
        }, cancellationToken: cancellationToken);

        return availableStores is null ? Enumerable.Empty<Guid>() : availableStores;
    }
}
