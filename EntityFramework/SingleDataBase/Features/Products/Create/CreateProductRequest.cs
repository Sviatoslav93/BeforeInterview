using System.ComponentModel.DataAnnotations;
using MediatR;
using Result;
using SingleDataBase.Entities;

namespace SingleDataBase.Features.Products.Create;

public class CreateProductRequest : IRequest<Result<int>>
{
    [Required]
    [Length(1, Product.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Length(1, Product.ImageUrlMaxLength)]
    public string? ImageUrl { get; set; }

    [Length(1, Product.DescriptionMaxLength)]
    public string? Description { get; set; }

    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    [Range(0.01, 1000000)]
    public decimal Quantity { get; set; }
    public QuantityType QuantityType { get; set; }
}
