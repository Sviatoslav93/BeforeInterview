using Result;

namespace Store.Errors;

public class AppError(string code, string message) : Error(message)
{
    public string Code { get; init; } = code;
}
