using Result;
using Store.Entities.Abstractions;

namespace Store.Entities;

public class Store : Entity<Guid>, IAggregate
{
    public const int NameMaxLength = 256;
    public const int WebsiteUriMaxLength = 1024;

#pragma warning disable CS8618 // Ef Core constructor
    private Store()
    {
    }
#pragma warning restore CS8618

    private Store(
        string name,
        Guid userId,
        string? websiteUri = null)
    {
        Name = name;
        UserId = userId;
        WebsiteUri = websiteUri;
    }

    public string Name { get; private set; }
    public string? WebsiteUri { get; private set; }
    public Guid UserId { get; private set; }

    public static Result<Store> Create(
        string name,
        Guid userId,
        string? websiteUri = null)
    {
        return new Store(name, userId, websiteUri);
    }
}
