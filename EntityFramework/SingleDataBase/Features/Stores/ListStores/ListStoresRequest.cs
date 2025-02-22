using MediatR;

namespace SingleDataBase.Features.Stores.ListStores;

public class ListStoresRequest : IRequest<List<StoreView>>
{
    public Guid UserId { get; set; }
}
