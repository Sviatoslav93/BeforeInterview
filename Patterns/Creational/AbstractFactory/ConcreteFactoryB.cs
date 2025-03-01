namespace AbstractFactory;

public class ConcreteFactoryB : AbstractFactory
{
    public override Product CreateProduct()
    {
        return new ConcreteProductB();
    }
}
