using Store.Entities.Abstractions;

namespace Store.Entities;

public class Product : Entity<int>, IAggregate, IStoreId
{

    public const int NameMaxLength = 256;
    public const int DescriptionMaxLength = 4000;
    public const int ImageUrlMaxLength = 1024;


#pragma warning disable CS8618 // Ef Core constructor
    private Product()
    {
    }
#pragma warning restore CS8618


    public Product(
        string name,
        decimal price,
        Quantity quantity,
        string? imageUrl = null,
        string? description = null)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        ImageUrl = imageUrl;
        Description = description;
    }

    public Guid StoreId { get; set; }
    public string Name { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public Quantity Quantity { get; private set; }

    public static Product Create(
        string name,
        decimal price,
        Quantity quantity,
        string? imageUrl = null,
        string? description = null)
    {
        return new Product(name, price, quantity, imageUrl, description);
    }
}
