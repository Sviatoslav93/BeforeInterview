using MediatR;
using Result;
using SingleDataBase.Database;

namespace SingleDataBase.Features.Deals.Delete;

public class DeleteDealHandler(StoreDbContext db) : IRequestHandler<DeleteDealRequest, Result<Unit>>
{

    public async Task<Result<Unit>> Handle(DeleteDealRequest request, CancellationToken cancellationToken)
    {
        var deal = await db.Deals.FindAsync([request.Id], cancellationToken: cancellationToken);
        if (deal == null)
        {
            return new Error("deal not found");
        }

        db.Deals.Remove(deal);
        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
