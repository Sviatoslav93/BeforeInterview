namespace HashTable;

/// <summary>
/// A simple hash table implementation.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
/// <param name="capacity"></param>
public class MyHashTable<TKey, TValue>(int capacity = 8)
{
    private readonly LinkedList<KeyValuePair<TKey, TValue>>[] _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];

    public void Add(TKey key, TValue value)
    {
        var index = GetIndex(key);
        if (_buckets[index] == null)
        {
            _buckets[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
        }

        var bucket = _buckets[index];
        foreach (var pair in bucket)
        {
            if (pair.Key!.Equals(key))
            {
                throw new ArgumentException("Key already exists");
            }
        }

        bucket.AddLast(new KeyValuePair<TKey, TValue>(key, value));
    }

    public bool Remove(TKey key)
    {
        var index = GetIndex(key);
        var bucket = _buckets[index];
        if (bucket == null)
        {
            return false;
        }

        var current = bucket.First;
        while (current != null)
        {
            if (current.Value.Key!.Equals(key))
            {
                bucket.Remove(current);
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public TValue TryGetValue(TKey key)
    {
        var index = GetIndex(key);
        var bucket = _buckets[index];
        if (bucket == null)
        {
            return default!;
        }

        foreach (var pair in bucket)
        {
            if (pair.Key!.Equals(key))
            {
                return pair.Value;
            }
        }

        return default!;
    }

    private int GetIndex(TKey? key)
    {
        return Math.Abs(key?.GetHashCode() ?? 0) % _buckets.Length;
    }
}
