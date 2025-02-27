using MediatR;
using Microsoft.EntityFrameworkCore;
using SingleDataBase.Database;

namespace SingleDataBase.Features.Deals.ListDeals;

public class ListDealHandler(StoreDbContext db) : IRequestHandler<ListDealsRequest, List<DealView>>
{
    private readonly StoreDbContext _db = db;

    public Task<List<DealView>> Handle(ListDealsRequest request, CancellationToken cancellationToken)
    {
        return _db.Deals
            .OrderByDescending(d => d.CreatedAt)
            .Skip(request.PageSize * request.PageNumber)
            .Take(request.PageSize)
            .Select(d => new DealView()
            {
                Id = d.Id,
                StoreCode = d.StoreCode,
                Status = d.Status,
                DeliveryDate = d.DeliveryDate,
                CreatedAt = d.CreatedAt,
                CreatedBy = d.CreatedBy,
                UpdatedAt = d.UpdatedAt,
                UpdatedBy = d.UpdatedBy,
                Notes = d.Notes
            })
            .ToListAsync(cancellationToken);
    }
}
