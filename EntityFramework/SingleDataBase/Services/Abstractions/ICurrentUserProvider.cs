namespace SingleDataBase.Services.Abstractions;

public interface ICurrentUserProvider
{
    Guid GetUserId();
    string GetEmail();
}

