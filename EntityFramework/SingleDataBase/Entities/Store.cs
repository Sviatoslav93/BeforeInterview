using SingleDataBase.Entities.Abstractions;

namespace SingleDataBase.Entities;

public class Store : Entity<Guid>, IAggregate
{
    #region Constants
    public const int NameMaxLength = 256;
    public const int WebsiteUriMaxLength = 1024;
    #endregion

    public required string Name { get; set; }
    public string? WebsiteUri { get; set; }
    public Guid UserId { get; set; }
}
