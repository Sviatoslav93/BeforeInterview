namespace AboutMyEnvironment;

internal class Program
{
    private static void Main()
    {
        var name = typeof(Program).Namespace ?? "null";
        Console.WriteLine(name);
    }
}