using SingleDataBase.Entities;

namespace SingleDataBase.Features.Deals.ListDeals;

public class DealView
{

    public int Id { get; set; }
    public Guid? StoreCode { get; set; }
    public DealStatus Status { get; set; }
    public DateTimeOffset DeliveryDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public string? Notes { get; set; }
}
