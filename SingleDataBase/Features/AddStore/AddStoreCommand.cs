using MediatR;

namespace SingleDataBase.Features.AddStore;

public class AddStoreCommand : IRequest<int>
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? WebsiteUri { get; set; }
}