using LanguageExt.Common;
using MediatR;
using SingleDataBase.Database;
using SingleDataBase.Exceptios;

namespace SingleDataBase.Features.Deals.DeleteDeal;

public class DeleteDealHandler(StoreDbContext db) : IRequestHandler<DeleteDealRequest, Result<Unit>>
{
    private readonly StoreDbContext _db = db;

    public async Task<Result<Unit>> Handle(DeleteDealRequest request, CancellationToken cancellationToken)
    {
        var deal = await _db.Deals.FindAsync([request.Id], cancellationToken: cancellationToken);
        if (deal == null)
        {
            return new Result<Unit>(new NotFoundException("Deal not found"));
        }

        _db.Deals.Remove(deal);
        await _db.SaveChangesAsync(cancellationToken);

        return new Result<Unit>(Unit.Default);
    }
}
