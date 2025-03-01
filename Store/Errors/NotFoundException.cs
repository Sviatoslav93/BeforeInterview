namespace Store.Errors;
public class NotFoundError(string message) : AppError("not-found", message)
{
}
