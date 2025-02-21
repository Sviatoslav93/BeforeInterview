namespace SingleDataBase.Entities;

public class Deal
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string? Notes { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset DeliveryDate { get; set; }
    public ICollection<Product> Products { get; set; } = null!;
}