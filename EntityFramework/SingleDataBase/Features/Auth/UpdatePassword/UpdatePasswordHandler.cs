using LanguageExt.Common;
using MediatR;
using SingleDataBase.Database;
using SingleDataBase.Exceptios;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Features.Auth.UpdatePassword;

public class UpdatePasswordHandler(StoreDbContext context, ICurrentUserProvider currentUserProvider) : IRequestHandler<UpdatePasswordRequest, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync([currentUserProvider.UserId], cancellationToken: cancellationToken);
        if (user == null)
        {
            return new Result<Unit>(new NotFoundException("User not found"));
        }

        if (user.Password == request.OldPassword)
        {
            return new Result<Unit>(new AppException("new-password-the-same-as-old", "Password is incorrect"));
        }

        user.Password = request.NewPassword;
        await context.SaveChangesAsync(cancellationToken);
        return new Result<Unit>(Unit.Default);
    }
}
