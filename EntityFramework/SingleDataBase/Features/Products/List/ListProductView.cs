using SingleDataBase.Entities;

namespace SingleDataBase.Features.Products.List;

public class ListProductView(
    int id,
    Guid storeId,
    string name,
    string? imageUrl,
    decimal price,
    QuantityType quantityType,
    decimal quantityValue
    )
{
    public int Id { get; set; } = id;
    public Guid StoreId { get; set; } = storeId;
    public string Name { get; set; } = name;
    public string? ImageUrl { get; set; } = imageUrl;
    public decimal Price { get; set; } = price;
    public QuantityType QuantityType { get; set; } = quantityType;
    public decimal QuantityValue { get; set; } = quantityValue;
}
