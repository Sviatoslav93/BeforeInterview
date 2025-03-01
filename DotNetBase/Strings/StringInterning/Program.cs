var a = "Hello world!";
var b = "Hello world!";

Console.WriteLine(ReferenceEquals(a, b)); // True

// but
var c = new string("Hello world!".ToCharArray());
Console.WriteLine(ReferenceEquals(a, c)); // False

var name = "world!";
var d = $"Hello {name}";
var e = $"Hello {name}";
Console.WriteLine(ReferenceEquals(d, e)); // False