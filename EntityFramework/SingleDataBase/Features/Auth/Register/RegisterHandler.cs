using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SingleDataBase.Database;
using SingleDataBase.Entities;
using SingleDataBase.Exceptios;

namespace SingleDataBase.Features.Auth.Register;

public class RegisterHandler(
    StoreDbContext db) : IRequestHandler<RegisterRequest, Result<Guid>>
{
    private readonly StoreDbContext _db = db;

    public async Task<Result<Guid>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var existUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (existUser is not null)
        {
            return new Result<Guid>(new AppException("user-already-exist", "User already exists"));
        }

        var user = new User
        {
            Id = Guid.NewGuid(),

            Email = request.Email,
            Password = request.Password,

            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
        };

        var entry = await _db.Users.AddAsync(user, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return entry.Entity.Id;
    }
}
