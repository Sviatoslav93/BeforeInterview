using Store.Entities;

namespace Store.Features.Products.GetInfo;

public class GetProductView(
    int id,
    string name,
    string? imageUrl,
    string? description,
    decimal price,
    QuantityType quantityType,
    decimal quantity
    )
{
    public int Id { get; init; } = id;
    public string Name { get; init; } = name;
    public string? ImageUrl { get; init; } = imageUrl;
    public string? Description { get; init; } = description;
    public decimal Price { get; init; } = price;
    public QuantityType QuantityType { get; init; } = quantityType;
    public decimal Quantity { get; init; } = quantity;
}
