using SingleDataBase.Entities.Abstractions;

namespace SingleDataBase.Entities;

public class Product : Entity<int>, IAggregate, IStoreId
{

    #region Constants
    public const int NameMaxLength = 256;
    public const int DescriptionMaxLength = 4000;

    #endregion

    public Guid StoreId { get; set; }
    public required string Name { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Quantity Quantity { get; set; }
}
