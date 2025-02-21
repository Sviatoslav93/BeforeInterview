using System.Text;

var sb = new StringBuilder();
sb.Append("Hello, ");
sb.Append("World!");
sb.Replace("World", "C#");

Console.WriteLine(sb.ToString());