using MediatR;
using Result;

namespace Store.Features.Auth.Login;

public class LoginRequest : IRequest<Result<LoginView>>
{
    public required string Email { get; set; }
    public required string Password { get; set; } = null!;
}
