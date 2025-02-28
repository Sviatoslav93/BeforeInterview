using SingleDataBase.Entities.Abstractions;
using Result;

namespace SingleDataBase.Entities;

public class User : Entity<Guid>, IAggregate
{
    public const int EmailMaxLength = 256;
    public const int FirstNameMaxLength = 256;
    public const int LastNameMaxLength = 256;
    public const int AddressMaxLength = 4000;
    public const int PasswordMaxLength = 256;

#pragma warning disable CS8618 // Ef Core constructor
    public User()
    {

    }
#pragma warning restore CS8618

    private User(
        string email,
        string password,
        string firstName,
        string lastName,
        DateTimeOffset dateOfBirth)
    {
        Email = email;
        Password = HashPassword(password);
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public string Email { get; private set; }
    public string Password { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTimeOffset DateOfBirth { get; private set; }

    public ICollection<Store> Stores { get; set; } = [];

    public string FullName => $"{FirstName} {LastName}";
    public int Age => CalculateAge();

    public Result<Unit> UpdatePassword(string oldPassword, string newPassword)
    {
        if (!VerifyPassword(oldPassword))
        {
            return new Error("old password is incorrect");
        }

        if (VerifyPassword(newPassword))
        {
            return new Error("new password is the same as the old password");
        }

        if (!IsPasswordStrong(newPassword))
        {
            return new Error("new password is not strong enough");
        }

        Password = HashPassword(newPassword);
        return Unit.Value;
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, Password);
    }

    private int CalculateAge()
    {
        int age = DateTime.Now.Year - DateOfBirth.Year;

        if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
        {
            age--;
        }
        return age;
    }

    public static Result<User> Create(
        string email,
        string password,
        string firstName,
        string lastName,
        DateTimeOffset dateOfBirth)
    {
        if (!IsPasswordStrong(password))
        {
            return new Error("Password is not strong enough");
        }

        return new User(email, password, firstName, lastName, dateOfBirth);
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool IsPasswordStrong(string password)
    {
        return password.Length >= 8;
    }
}
