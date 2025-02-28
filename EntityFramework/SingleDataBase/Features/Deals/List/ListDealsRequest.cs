using System.ComponentModel.DataAnnotations;
using MediatR;
using Result;

namespace SingleDataBase.Features.Deals.List;

public class ListDealsRequest : IRequest<Result<List<DealView>>>
{
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }

    [Range(1, 50)]
    public int PageSize { get; set; }
}
