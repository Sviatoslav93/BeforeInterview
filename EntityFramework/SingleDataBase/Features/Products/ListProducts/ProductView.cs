using SingleDataBase.Entities;

namespace SingleDataBase.Features.Products.ListProducts;

public class ProductView
{
    public Guid StoreId { get; set; }
    public required string Name { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public QuantityType QuantityType { get; set; }
    public decimal QuantityValue { get; set; }
}
