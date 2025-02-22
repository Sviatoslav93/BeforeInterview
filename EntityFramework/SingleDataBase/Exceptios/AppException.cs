namespace SingleDataBase.Exceptios;

public class AppException : Exception
{
    public AppException(string code, string message) : base(message)
    {
        Code = code;
    }
    public string Code { get; init; }
}
