using SingleDataBase.Entities.Abstractions;

namespace SingleDataBase.Entities;

public class Deal : IAggregate, IStoreCode, IAuditableEntity
{
    #region Constants
    public const int NotesMaxLength = 256;
    #endregion

    public int Id { get; set; }
    public Guid StoreCode { get; set; }
    public required DealStatus Status { get; set; }
    public required DateTimeOffset DeliveryDate { get; set; }
    public ICollection<DealProduct> Products { get; set; } = [];
    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public string? Notes { get; set; }
}
