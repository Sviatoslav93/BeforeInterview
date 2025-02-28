namespace SingleDataBase.Exceptios;

public class IncorrectCredentialsException() : AppException("incorrect-credentials", "Email or password is incorrect")
{
}
