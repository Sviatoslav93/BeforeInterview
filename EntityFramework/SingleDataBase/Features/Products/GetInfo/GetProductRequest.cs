using MediatR;
using Result;

namespace SingleDataBase.Features.Products.GetInfo;

public class GetProductRequest : IRequest<Result<GetProductView>>
{
    public int Id { get; set; }
}
