using SingleDataBase.Entities;

namespace SingleDataBase.Features.Deals.List;

public class DealView(
    int id,
    Guid? storeId,
    DealStatus status,
    DateTimeOffset deliveryDate,
    DateTimeOffset createdAt,
    Guid createdBy,
    DateTimeOffset? updatedAt,
    Guid? updatedBy,
    string? notes)
{
    public int Id { get; set; } = id;
    public Guid? StoreId { get; set; } = storeId;
    public DealStatus Status { get; set; } = status;
    public DateTimeOffset DeliveryDate { get; set; } = deliveryDate;
    public DateTimeOffset CreatedAt { get; set; } = createdAt;
    public Guid CreatedBy { get; set; } = createdBy;
    public DateTimeOffset? UpdatedAt { get; set; } = updatedAt;
    public Guid? UpdatedBy { get; set; } = updatedBy;
    public string? Notes { get; set; } = notes;
}
