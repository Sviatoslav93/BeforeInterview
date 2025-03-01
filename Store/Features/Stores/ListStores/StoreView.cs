namespace Store.Features.Stores.ListStores;

public class StoreView(
    Guid id,
    string name,
    string? websiteUri)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string? WebsiteUri { get; set; } = websiteUri;
}
