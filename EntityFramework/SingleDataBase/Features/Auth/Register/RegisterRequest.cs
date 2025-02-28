using MediatR;
using Result;

namespace SingleDataBase.Features.Auth.Register;

public class RegisterRequest : IRequest<Result<Guid>>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTimeOffset DateOfBirth { get; set; }
}
