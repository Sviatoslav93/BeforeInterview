using MediatR;

namespace SingleDataBase.Features.Stores.CreateStore;

public class CreateStoreRequest : IRequest<Guid>
{
    public required string Name { get; set; }
    public required string WebsiteUri { get; set; }
}
