using MediatR;

namespace SingleDataBase.Features.Stores.Events;

public class StoreCreated : INotification
{
    public required string StoreCode { get; set; }
    public Guid UserId { get; set; }
}
