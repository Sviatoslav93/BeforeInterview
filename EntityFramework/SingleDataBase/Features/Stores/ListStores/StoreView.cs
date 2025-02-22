namespace SingleDataBase.Features.Stores.ListStores;

public class StoreView
{
    public Guid StoreCode { get; set; }
    public required string Name { get; set; }
    public string? WebsiteUri { get; set; }
}
