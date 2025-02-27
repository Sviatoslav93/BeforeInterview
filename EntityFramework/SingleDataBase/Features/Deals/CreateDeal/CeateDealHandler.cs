using MediatR;
using SingleDataBase.Database;
using SingleDataBase.Entities;

namespace SingleDataBase.Features.Deals.CreateDeal;

public class CeateDealHandler(StoreDbContext context) : IRequestHandler<CreateDealRequest, int>
{
    public async Task<int> Handle(CreateDealRequest request, CancellationToken cancellationToken)
    {
        var deal = new Deal
        {
            Status = DealStatus.Pending,
            Notes = request.Notes,
            DeliveryDate = request.DeliveryDate,
        };

        foreach (var product in request.Products)
        {
            // todo: check if product exists and is available
            deal.Products.Add(new DealProduct(product.ProductId, product.Quantity));
        }

        await context.Deals.AddAsync(deal, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return deal.Id;
    }
}
