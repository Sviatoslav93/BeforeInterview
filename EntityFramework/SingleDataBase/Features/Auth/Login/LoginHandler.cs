using MediatR;
using Microsoft.EntityFrameworkCore;
using Result;
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
            return new Error("incorrect credentials");
        }

        if (user.VerifyPassword(request.Password))
        {
            return new LoginView
            {
                Id = user.Id.ToString(),
                Email = user.Email,
            };
        }

        return new Error("incorrect credentials");
    }
}
