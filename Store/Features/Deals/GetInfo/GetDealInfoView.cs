using System.ComponentModel.DataAnnotations;
using Store.Entities;

namespace Store.Features.Deals.GetInfo;

public class DealInfoView(
    string? notes,
    DealStatus status,
    DateTimeOffset deliveryDate,
    IEnumerable<DealInfoView.ProductInfoView> products)
{
    public string? Notes { get; init; } = notes;
    public DealStatus Status { get; init; } = status;
    public DateTimeOffset DeliveryDate { get; init; } = deliveryDate;
    public IEnumerable<ProductInfoView> Products { get; init; } = products;

    public class ProductInfoView(
        string name,
        string? imageUrl,
        string? description,
        decimal price,
        QuantityType quantityType,
        decimal quantity)
    {
        [Required]
        public string Name { get; init; } = name;
        public string? ImageUrl { get; init; } = imageUrl;

        public string? Description { get; init; } = description;
        public decimal Price { get; init; } = price;
        public QuantityType QuantityType { get; init; } = quantityType;
        public decimal Quantity { get; init; } = quantity;
    }
}
