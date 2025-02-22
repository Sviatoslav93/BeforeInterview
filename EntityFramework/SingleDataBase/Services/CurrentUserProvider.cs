using System.IdentityModel.Tokens.Jwt;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Services;

public class CurrentUserProvider(IHttpContextAccessor httpContextAccessor) : ICurrentUserProvider
{
    public string GetEmail()
    {
        var context = httpContextAccessor.HttpContext;
        return (context?.User.Claims.FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.Email))?.Value)
             ?? throw new InvalidOperationException("User id not found in claims");
    }

    public Guid GetUserId()
    {
        var context = httpContextAccessor.HttpContext;
        var id = (context?.User.Claims.FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.Sub))?.Value)
             ?? throw new InvalidOperationException("User id not found in claims");
        return Guid.Parse(id);
    }
}
