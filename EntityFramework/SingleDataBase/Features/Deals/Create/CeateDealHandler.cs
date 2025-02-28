using MediatR;
using Result;
using Result.Extensions;
using SingleDataBase.Database;
using SingleDataBase.Entities;

namespace SingleDataBase.Features.Deals.Create;

public class CeateDealHandler(StoreDbContext db) : IRequestHandler<CreateDealRequest, Result<int>>
{
    public async Task<Result<int>> Handle(CreateDealRequest request, CancellationToken cancellationToken)
    {
        return await Deal.Create(
            deliveryDate: request.DeliveryDate,
            notes: request.Notes)
            .Then(deal =>
            {
                foreach (var product in request.Products)
                {
                    deal.Products.Add(new DealProduct(product.ProductId, product.Quantity));
                }

                return deal;
            })
            .ThenAsync(
                async deal =>
                {
                    var entry = await db.Deals.AddAsync(deal, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return entry.Entity.Id;
                }
            );
    }
}
