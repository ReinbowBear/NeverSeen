using System;
using System.Collections.Generic;

public sealed class SparseSet<TKey> where TKey : IEquatable<TKey>
{
    private readonly Dictionary<TKey, int> sparse;
    private TKey[] dense;
    private int count;

    public int Count => count;
    //public StructEnumerator<TKey> GetEnumerator() => new StructEnumerator<TKey>(dense, count);
    public TKey this[int index] => dense[index];


    public SparseSet(int initialCapacity = 16)
    {
        sparse = new Dictionary<TKey, int>(initialCapacity);
        dense = new TKey[Math.Max(initialCapacity, 16)];
    }


    public void Add(TKey key)
    {
        if (sparse.ContainsKey(key)) return;

        if (count >= dense.Length)
        {
            Array.Resize(ref dense, dense.Length * 2);
        }

        dense[count] = key;
        sparse[key] = count;
        count++;
    }

    public void Remove(TKey key)
    {
        if (!sparse.TryGetValue(key, out int index)) return;

        int last = count - 1;
        if (index != last)
        {
            var lastKey = dense[last];
            dense[index] = lastKey;
            sparse[lastKey] = index;
        }

        sparse.Remove(key);
        count--;
    }


    public bool Contains(TKey key)
    {
        return sparse.ContainsKey(key);
    }

    public int IndexOf(TKey key)
    {
        return sparse.TryGetValue(key, out int index) ? index : -1;
    }


    public void Clear()
    {
        sparse.Clear();
        count = 0;
    }
}
