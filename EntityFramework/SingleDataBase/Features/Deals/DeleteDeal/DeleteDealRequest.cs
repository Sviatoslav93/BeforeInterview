using LanguageExt.Common;
using MediatR;

namespace SingleDataBase.Features.Deals.DeleteDeal;

public class DeleteDealRequest : IRequest<Result<Unit>>
{
    public int Id { get; set; }
}
