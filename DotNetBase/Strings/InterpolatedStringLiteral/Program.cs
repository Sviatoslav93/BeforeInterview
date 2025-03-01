
var firstName = "John";
var lastName = "Doe";
var age = 25;

var request = $$"""
{
    "firstName": "{{firstName}}",
    "lastName": "{{lastName}}",
    "age": {{age}}
}
""";

var sql = $$"""
select * from users where firstName = '{{firstName}}' and lastName = '{{lastName}}'
""";

Console.WriteLine(request);
Console.WriteLine(sql);