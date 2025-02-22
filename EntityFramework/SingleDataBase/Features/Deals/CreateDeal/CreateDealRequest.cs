using MediatR;

namespace SingleDataBase.Features.Deals.CreateDeal;

public class CreateDealRequest : IRequest<int>
{
    public string? Notes { get; set; }
    public DateTimeOffset DeliveryDate { get; set; }
    public ICollection<CreateDealProductModel> Products { get; set; } = [];
}
