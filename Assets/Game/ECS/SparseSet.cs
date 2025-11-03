using System;
using System.Collections.Generic;

public sealed class SparseSet<TKey> where TKey : IEquatable<TKey> // класс заменяющий лист, делающий Contains и Add\Remove О(1)
{
    private readonly Dictionary<TKey, int> sparse; // для поиска по ключу
    private TKey[] dense; // для итерации
    private int count;

    public int Count => count;
    public ReadOnlySpan<TKey> AsSpan() => dense.AsSpan(0, count);

    public SparseSet(int initialCapacity = 16)
    {
        sparse = new Dictionary<TKey, int>(initialCapacity);
        dense = new TKey[Math.Max(initialCapacity, 16)];
        count = 0;
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

        var last = dense[count - 1];
        dense[index] = last;
        sparse[last] = index;

        sparse.Remove(key);
        count--;
    }

    public bool Contains(TKey key)
    {
        return sparse.ContainsKey(key);
    }


    public void Clear()
    {
        sparse.Clear();
        count = 0;
    }
}
