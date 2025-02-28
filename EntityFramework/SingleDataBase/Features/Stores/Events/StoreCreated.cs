using MediatR;

namespace SingleDataBase.Features.Stores.Events;

public readonly record struct StoreCreatedEvent(Guid StoreId, Guid UserId) : INotification;

