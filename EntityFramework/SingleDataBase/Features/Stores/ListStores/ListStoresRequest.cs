using MediatR;
using Result;

namespace SingleDataBase.Features.Stores.ListStores;

public class ListStoresRequest : IRequest<Result<List<StoreView>>>
{
    public Guid UserId { get; set; }
}
