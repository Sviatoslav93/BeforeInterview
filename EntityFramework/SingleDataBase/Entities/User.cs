namespace SingleDataBase.Entities;

public class User
{
    #region Constants
    public const int EmailMaxLength = 256;
    public const int FirstNameMaxLength = 256;
    public const int LastNameMaxLength = 256;
    public const int AddressMaxLength = 4000;
    public const int PasswordMaxLength = 256;
    #endregion

    public Guid Id { get; set; }
    public ICollection<Store> Stores { get; set; } = [];
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTimeOffset DateOfBirth { get; set; }

    public string FullName => $"{FirstName} {LastName}";
    public int Age => CalculateAge();

    private int CalculateAge()
    {
        int age = DateTime.Now.Year - DateOfBirth.Year;

        if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
        {
            age--;
        }
        return age;
    }
}
