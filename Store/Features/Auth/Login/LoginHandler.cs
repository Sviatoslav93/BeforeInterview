using MediatR;
using Microsoft.EntityFrameworkCore;
using Result;
using Store.Database;

namespace Store.Features.Auth.Login;

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
