using MediatR;

namespace SingleDataBase.Features.Deals.ListDeals;

public class ListDealsRequest : IRequest<List<DealView>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
