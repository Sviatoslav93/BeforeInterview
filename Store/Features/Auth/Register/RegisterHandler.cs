using MediatR;
using Microsoft.EntityFrameworkCore;
using Result;
using Result.Extensions;
using Store.Database;
using Store.Entities;

namespace Store.Features.Auth.Register;

public class RegisterHandler(StoreDbContext db) : IRequestHandler<RegisterRequest, Result<Guid>>
{
    private readonly StoreDbContext _db = db;

    public async Task<Result<Guid>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var existUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (existUser is not null)
        {
            return new Error("user already exists");
        }

        return await User.Create(
            email: request.Email,
            password: request.Password,
            firstName: request.FirstName,
            lastName: request.LastName,
            dateOfBirth: request.DateOfBirth)
                .ThenAsync(async user =>
                {
                    var entry = await _db.Users.AddAsync(user, cancellationToken);
                    await _db.SaveChangesAsync(cancellationToken);

                    return entry.Entity.Id;
                });
    }
}
