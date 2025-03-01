using MediatR;

namespace Store.Features.Stores.Events;

public readonly record struct StoreCreatedEvent(Guid StoreId, Guid UserId) : INotification;

