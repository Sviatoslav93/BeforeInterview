using MediatR;
using Result;
using Result.Extensions;
using Store.Database;
using Store.Services.Abstractions;

namespace Store.Features.Auth.UpdatePassword;

public class UpdatePasswordHandler(
    StoreDbContext context,
    ICurrentUserProvider currentUserProvider) : IRequestHandler<UpdatePasswordRequest, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync([currentUserProvider.UserId], cancellationToken: cancellationToken);
        if (user == null)
        {
            return new Error("user not found");
        }

        return await user.UpdatePassword(request.OldPassword, request.NewPassword)
            .ThenAsync(_ => context.SaveChangesAsync(cancellationToken))
            .ThenAsync(_ => Unit.Value);
    }
}
