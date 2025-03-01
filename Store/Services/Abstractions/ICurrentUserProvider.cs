namespace Store.Services.Abstractions;

public interface ICurrentUserProvider
{
    Guid UserId { get; }
}

