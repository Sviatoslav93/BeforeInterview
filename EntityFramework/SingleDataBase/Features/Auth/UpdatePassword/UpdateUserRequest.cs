using LanguageExt.Common;
using MediatR;

namespace SingleDataBase.Features.Auth.UpdatePassword;

public class UpdatePasswordRequest : IRequest<Result<Unit>>
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}
