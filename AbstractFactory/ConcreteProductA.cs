namespace AbstractFactory;

public class ConcreteProductA : Product
{
    public override string GetDetails()
    {
        return "ConcreteProductA created.";
    }
}