using HashTable;

var hasTable = new MyHashTable<string, Person>(4);

hasTable.Add("John", new Person("John", "Doe", 25));
hasTable.Add("Jane", new Person("Jane", "Doe", 22));
hasTable.Add("Jak", new Person("Jack", "Doe", 20));
hasTable.Add("Jill", new Person("Jill", "Doe", 18));
hasTable.Add("Jim", new Person("Jim", "Doe", 16));
hasTable.Add("Jenny", new Person("Jenny", "Doe", 14));
hasTable.Add("Jerry", new Person("Jerry", "Doe", 12));
hasTable.Add("Jesse", new Person("Jesse", "Doe", 11));

// hasTable.Add("Jim", new Person("Jim", "Doe", 16)); // Throws ArgumentException: Key already exists

if (hasTable.TryGetValue("Jim Doe") is Person person)
{
    Console.WriteLine(person);
}
