using SingleDataBase.Entities.Abstractions;

namespace SingleDataBase.Entities;

public class Product : IAggregate, IStoreCode
{

    #region Constants
    public const int NameMaxLength = 256;
    public const int DescriptionMaxLength = 4000;

    #endregion

    public int Id { get; set; }
    public required Guid? StoreCode { get; set; }
    public int DealId { get; set; }
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Quantity Quantity { get; set; }
}
