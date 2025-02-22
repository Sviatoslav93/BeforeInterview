using MediatR;
using SingleDataBase.Database;
using SingleDataBase.Entities;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Features.Deals.CreateDeal;

public class CeateDealHandler(
    StoreDbContext context,
    IStoreCodeProvider storeCodeProvider,
    TimeProvider timeProvider) : IRequestHandler<CreateDealRequest, int>
{
    private readonly StoreDbContext _context = context;
    private readonly IStoreCodeProvider _storeCodeProvider = storeCodeProvider;
    private readonly TimeProvider _timeProvider = timeProvider;

    public async Task<int> Handle(CreateDealRequest request, CancellationToken cancellationToken)
    {
        var storeCode = _storeCodeProvider.GetCurrentStoreCode();
        var deal = new Deal
        {
            StoreCode = storeCode,
            Status = DealStatus.Pending,
            Notes = request.Notes,
            DeliveryDate = request.DeliveryDate,
            CreatedAt = _timeProvider.GetUtcNow(),
            CreatedBy = Guid.NewGuid(),
            UpdatedBy = Guid.NewGuid()
        };

        foreach (var product in request.Products)
        {
            // todo: check if product exists and is available
            deal.Products.Add(new DealProduct(product.ProductId, product.Quantity));
        }

        await _context.Deals.AddAsync(deal, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return deal.Id;
    }
}
