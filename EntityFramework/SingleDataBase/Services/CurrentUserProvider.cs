using System.Security.Claims;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Services;

public class CurrentUserProvider(IHttpContextAccessor httpContextAccessor) : ICurrentUserProvider
{
    private Guid? _userId = null;
    public Guid UserId => _userId ??= GetUserId();

    private Guid GetUserId()
    {
        var context = httpContextAccessor.HttpContext;
        var id = (context?.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value)
             ?? throw new InvalidOperationException("User id not found in claims");
        return Guid.Parse(id);
    }
}
