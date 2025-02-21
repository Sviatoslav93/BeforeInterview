namespace AbstractFactory;

public class ConcreteFactoryA : AbstractFactory
{
    public override Product CreateProduct()
    {
        return new ConcreteProductA();
    }
}