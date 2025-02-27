namespace SingleDataBase.Services.Abstractions;

public interface ICurrentUserProvider
{
    Guid UserId { get; }
}

