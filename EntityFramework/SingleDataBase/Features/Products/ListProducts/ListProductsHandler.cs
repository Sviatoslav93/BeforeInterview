using MediatR;
using Microsoft.EntityFrameworkCore;
using SingleDataBase.Database;

namespace SingleDataBase.Features.Products.ListProducts;

public class ListProductsHandler : IRequestHandler<ListProductsRequest, List<ProductView>>
{
    private readonly StoreDbContext _db;

    public ListProductsHandler(StoreDbContext db)
    {
        _db = db;
    }

    public Task<List<ProductView>> Handle(ListProductsRequest request, CancellationToken cancellationToken)
    {
        return _db.Products
            .OrderBy(p => p.Name)
            .Skip(request.PageSize * request.PageNumber)
            .Take(request.PageSize)
            .Select(p => new ProductView()
            {
                StoreId = p.StoreId,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Price = p.Price,
                QuantityType = p.Quantity.Type,
                QuantityValue = p.Quantity.Value
            })
            .ToListAsync(cancellationToken);
    }
}
