namespace AbstractFactory;

public class Client(AbstractFactory factory)
{
    private readonly Product _product = factory.CreateProduct();

    public void ShowProductDetails()
    {
        Console.WriteLine(_product.GetDetails());
    }
}