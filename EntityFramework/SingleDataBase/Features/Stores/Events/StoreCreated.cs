using MediatR;

namespace SingleDataBase.Features.Stores.Events;

public class StoreCreated : INotification
{
    public string StoreCode { get; set; }
    public Guid UserId { get; set; }
}
