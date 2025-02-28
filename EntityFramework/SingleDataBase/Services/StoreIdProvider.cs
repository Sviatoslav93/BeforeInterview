using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Services;

public class StoreIdProvider(IHttpContextAccessor httpContextAccessor) : IStoreIdProvider
{
    private const string StoreIdHeader = "X-StoreId";

    private Guid? _storeId = null;
    public Guid StoreId => _storeId ??= GetCurrentStoreId();

    private Guid GetCurrentStoreId()
    {
        var storeId = httpContextAccessor.HttpContext?.Request.Headers[StoreIdHeader];

        if (!storeId.HasValue)
        {
            throw new ApplicationException("Store id not found in headers");
        }

        return Guid.Parse(storeId!);
    }
}
