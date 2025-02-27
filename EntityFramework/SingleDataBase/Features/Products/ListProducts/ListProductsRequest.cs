using MediatR;

namespace SingleDataBase.Features.Products.ListProducts;

public class ListProductsRequest : IRequest<List<ProductView>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
