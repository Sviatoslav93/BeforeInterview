using MediatR;
using Result;

namespace Store.Features.Deals.Delete;

public class DeleteDealRequest : IRequest<Result<Unit>>
{
    public int Id { get; set; }
}
