using MediatR;
using Microsoft.EntityFrameworkCore;
using Result;
using Store.Database;
using static Store.Features.Deals.GetInfo.DealInfoView;

namespace Store.Features.Deals.GetInfo;

public class GetDealInfoHandler(StoreDbContext db) : IRequestHandler<GetDealInfoRequest, Result<DealInfoView>>
{
    public async Task<Result<DealInfoView>> Handle(GetDealInfoRequest request, CancellationToken cancellationToken)
    {
        var deal = await db.Deals.FindAsync([request.Id], cancellationToken);
        if (deal == null)
        {
            return new Error("deal not found");
        }

        var productsIds = deal.Products.Select(p => p.ProductId).ToList();
        var productInfo = await db.Products
            .Where(p => productsIds.Contains(p.Id))
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.ImageUrl,
                p.Description,
                p.Price,
                QuantityType = p.Quantity.Type,
                Quantity = p.Quantity.Value
            })
            .ToListAsync(cancellationToken);

        return new DealInfoView(
           deal.Notes,
            deal.Status,
            deal.DeliveryDate,
             productInfo.Select(p => new DealInfoView.ProductInfoView(
                 p.Name,
                 p.ImageUrl,
                 p.Description,
                 p.Price,
                 p.QuantityType,
                 p.Quantity
            ))
        );
    }
}
