namespace SingleDataBase.Exceptios;

public class EmailPasswordMismatchException() : AppException("email_and_password_mismatch", "Email and password do not match")
{
}
