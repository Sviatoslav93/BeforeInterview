using MediatR;

namespace SingleDataBase.Features.ListStores;

public class ListStoresQuery : IRequest<List<ListStoreView>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

