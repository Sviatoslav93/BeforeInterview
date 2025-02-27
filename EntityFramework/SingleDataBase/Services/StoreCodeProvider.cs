using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Services;

public class StoreCodeProvider(IHttpContextAccessor httpContextAccessor) : IStoreCodeProvider
{
    private const string StoreCodeHeader = "X-StoreCode";

    private Guid? _storeCode = null;
    public Guid StoreCode => _storeCode ??= GetCurrentStoreCode();

    private Guid GetCurrentStoreCode()
    {
        var storeCode = httpContextAccessor.HttpContext?.Request.Headers[StoreCodeHeader];

        if (!storeCode.HasValue)
        {
            throw new ApplicationException("Store code not found in headers");
        }

        return Guid.Parse(storeCode!);
    }
}
