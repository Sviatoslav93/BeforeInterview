using LanguageExt.Common;
using MediatR;
using SingleDataBase.Entities;

namespace SingleDataBase.Features.Products.CreateProduct;

public class CreateProductRequest : IRequest<Result<int>>
{
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public QuantityType QuantityType { get; set; }
}
