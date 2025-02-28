using MediatR;
using Result;

namespace SingleDataBase.Features.Auth.Login;

public class LoginRequest : IRequest<Result<LoginView>>
{
    public required string Email { get; set; }
    public required string Password { get; set; } = null!;
}
