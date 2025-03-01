using System.ComponentModel.DataAnnotations;
using MediatR;
using Result;

namespace Store.Features.Products.List;

public class ListProductsRequest : IRequest<Result<List<ListProductView>>>
{
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }

    [Range(1, 50)]
    public int PageSize { get; set; }
}
