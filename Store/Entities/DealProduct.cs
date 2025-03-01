namespace Store.Entities;

public class DealProduct(int productId, decimal quantity)
{
    public int ProductId { get; init; } = productId;
    public decimal Quantity { get; init; } = quantity;
}
