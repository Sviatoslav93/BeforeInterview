using LanguageExt.Common;
using MediatR;

namespace SingleDataBase.Features.Auth.Login;

public class LoginRequest : IRequest<Result<LoginView>>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
