namespace SingleDataBase.Entities;

public class Store
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string WebsiteUri { get; set; } = null!;
    public ICollection<Deal> Deals { get; set; } = [];
}