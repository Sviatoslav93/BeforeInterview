namespace Store.Services.Abstractions;

public interface IStoreIdProvider
{
    Guid StoreId { get; }
}
