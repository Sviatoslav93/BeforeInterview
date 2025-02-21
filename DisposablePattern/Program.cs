using DisposablePattern;

Console.WriteLine("Example of Disposable Pattern");

using (var resourceHolder = new ResourceHolder("files/test.txt"))
{
    var content = resourceHolder.ReadFile();
    Console.WriteLine(content);
}

Console.ReadLine();
