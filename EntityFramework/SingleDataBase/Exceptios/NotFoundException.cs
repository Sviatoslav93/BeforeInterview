namespace SingleDataBase.Exceptios;

public class NotFoundException(string message) : AppException("not_found", message)
{
}
