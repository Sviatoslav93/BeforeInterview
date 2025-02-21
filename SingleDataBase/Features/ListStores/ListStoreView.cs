namespace SingleDataBase.Features.ListStores;

public class ListStoreView
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? WebsiteUri { get; set; }
}