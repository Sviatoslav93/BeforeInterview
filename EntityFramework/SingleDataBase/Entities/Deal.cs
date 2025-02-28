using Result;
using SingleDataBase.Entities.Abstractions;

namespace SingleDataBase.Entities;

public class Deal : AuditableEntity<int>, IAggregate, IStoreId
{

#pragma warning disable CS8618 // Ef Core constructor
    private Deal()
    {
    }
#pragma warning restore CS8618


    private Deal(
        DateTimeOffset deliveryDate,
        string? notes = null)
    {
        Status = DealStatus.Pending;
        DeliveryDate = deliveryDate;
        Notes = notes;
    }

    public const int NotesMaxLength = 256;

    public Guid StoreId { get; set; }
    public string? Notes { get; private set; }
    public DealStatus Status { get; private set; }
    public DateTimeOffset DeliveryDate { get; private set; }
    public ICollection<DealProduct> Products { get; private set; } = [];

    public static Result<Deal> Create(
        DateTimeOffset deliveryDate,
        string? notes = null)
    {
        return new Deal(deliveryDate, notes);
    }
}
