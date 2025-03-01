namespace Store.Entities.Abstractions;

public interface IAuditableEntity
{
    DateTimeOffset CreatedAt { get; }
    Guid CreatedBy { get; }
    DateTimeOffset? UpdatedAt { get; }
    Guid? UpdatedBy { get; }

    void SetCreateInfo(DateTimeOffset createdAt, Guid createdBy);
    void SetUpdateInfo(DateTimeOffset updatedAt, Guid updatedBy);
}

public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity where T : struct, IEquatable<T>
{
    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }

    public void SetCreateInfo(DateTimeOffset createdAt, Guid createdBy)
    {
        if (CreatedAt != default || createdBy != default)
        {
            throw new InvalidOperationException("Entity already created.");
        }

        CreatedAt = createdAt;
        CreatedBy = createdBy;
    }

    public void SetUpdateInfo(DateTimeOffset updatedAt, Guid updatedBy)
    {
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
    }
}
