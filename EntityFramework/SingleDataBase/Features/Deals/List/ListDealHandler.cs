using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Result;
using Result.Extensions;
using SingleDataBase.Database;
using SingleDataBase.Entities;
using SingleDataBase.Utils;

namespace SingleDataBase.Features.Deals.List;

public class ListDealHandler(StoreDbContext db) : IRequestHandler<ListDealsRequest, Result<List<DealView>>>
{
    public async Task<Result<List<DealView>>> Handle(ListDealsRequest request, CancellationToken cancellationToken)
    {
        var count = await db.Deals.CountAsync(cancellationToken);

        return await PaginationHelper.GetFromTo(request.PageNumber, request.PageSize, count)
            .ThenAsync(
                async p =>
                {
                    return await db.Deals
                        .OrderByDescending(x => x.CreatedAt)
                        .Skip(p.From)
                        .Take(p.To - p.From)
                        .Select(ToDealView)
                        .ToListAsync(cancellationToken);
                }
            );
    }

    public static Expression<Func<Deal, DealView>> ToDealView => x => new DealView(
        x.Id,
        x.StoreId,
        x.Status,
        x.DeliveryDate,
        x.CreatedAt,
        x.CreatedBy,
        x.UpdatedAt,
        x.UpdatedBy,
        x.Notes
    );
}
