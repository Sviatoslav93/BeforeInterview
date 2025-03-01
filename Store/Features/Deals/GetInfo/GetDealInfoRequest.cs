using System.ComponentModel.DataAnnotations;
using MediatR;
using Result;

namespace Store.Features.Deals.GetInfo;

// public record GetDealInfoRequest(int Id) : IRequest<Result<DealInfoView>>;
public class GetDealInfoRequest : IRequest<Result<DealInfoView>>
{
    [Required]
    public int Id { get; set; }
}
