namespace AbstractFactory;

internal static class Program
{
    public static void Main(string[] args)
    {
        // Use Factory A
        AbstractFactory factoryA = new ConcreteFactoryA();
        var clientA = new Client(factoryA);
        clientA.ShowProductDetails();

        // Use Factory B
        AbstractFactory factoryB = new ConcreteFactoryB();
        var clientB = new Client(factoryB);
        clientB.ShowProductDetails();
    }
}