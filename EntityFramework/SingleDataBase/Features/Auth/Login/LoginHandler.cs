using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SingleDataBase.Database;
using SingleDataBase.Exceptios;

namespace SingleDataBase.Features.Auth.Login;

public class LoginHandler(StoreDbContext db) : IRequestHandler<LoginRequest, Result<LoginView>>
{
    private readonly StoreDbContext _db = db;

    public async Task<Result<LoginView>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user == null)
        {
            return new Result<LoginView>(new EmailPasswordMismatchException());
        }

        if (string.Equals(user.Password, HashPassword(request.Password), StringComparison.Ordinal))
        {
            return new LoginView
            {
                Id = user.Id.ToString(),
                Email = user.Email,
            };
        }

        return new Result<LoginView>(new EmailPasswordMismatchException());
    }

    private static string HashPassword(string password)
    {
        return password;
    }
}
