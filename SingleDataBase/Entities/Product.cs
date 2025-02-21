namespace SingleDataBase.Entities;

public class Product
{
    public int Id { get; set; }
    public int DealId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}