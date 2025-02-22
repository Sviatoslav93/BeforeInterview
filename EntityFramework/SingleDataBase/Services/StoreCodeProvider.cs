using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Services;

public class StoreCodeProvider(IHttpContextAccessor httpContextAccessor) : IStoreCodeProvider
{
    private const string StoreCodeHeader = "X-StoreCode";

    public Guid GetCurrentStoreCode()
    {
        var storeCode = httpContextAccessor.HttpContext?.Request.Headers[StoreCodeHeader];

        if (!storeCode.HasValue)
        {
            throw new ApplicationException("Store code not found in headers");
        }

        return Guid.Parse(storeCode!);
    }
}
