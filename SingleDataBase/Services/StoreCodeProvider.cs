using SingleDataBase.Exceptios;
using SingleDataBase.Services.Abstractions;
using ApplicationException = SingleDataBase.Exceptios.ApplicationException;

namespace SingleDataBase.Services;

public class StoreCodeProvider(IHttpContextAccessor httpContextAccessor) : IStoreCodeProvider
{
    private const string StoreCodeHeader = "X-StoreCode";
    public string GetCurrentStoreCode()
    {
        return GetStoreCodeFromHeaders();
    }

    private string GetStoreCodeFromHeaders()
    {
        var storeCode = httpContextAccessor.HttpContext?.Request.Headers[StoreCodeHeader];

        if (!storeCode.HasValue)
        {
            throw new ApplicationException("Store code not found in headers");
        }

        return storeCode;
    }
}