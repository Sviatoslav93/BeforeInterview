using MediatR;
using Result;

namespace Store.Features.Auth.UpdatePassword;

public class UpdatePasswordRequest : IRequest<Result<Unit>>
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}
