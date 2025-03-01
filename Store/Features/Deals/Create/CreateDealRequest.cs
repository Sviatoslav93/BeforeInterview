using System.ComponentModel.DataAnnotations;
using MediatR;
using Result;
using Store.Entities;

namespace Store.Features.Deals.Create;

public class CreateDealRequest : IRequest<Result<int>>
{
    [Range(1, Deal.NotesMaxLength)]
    public string? Notes { get; set; }

    public DateTimeOffset DeliveryDate { get; set; }
    public required ICollection<CreateDealProductModel> Products { get; set; }

    public class CreateDealProductModel
    {
        public int ProductId { get; set; }

        [Range(1, 1000000)]
        public decimal Quantity { get; set; }
    }
}
