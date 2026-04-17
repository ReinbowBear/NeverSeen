using System;
using System.Collections.Generic;

public class SparseSet<T> where T : IEquatable<T>
{
    private readonly Dictionary<T, int> sparse;
    private T[] dense;
    private int count;

    public int Count => count;
    public ref T this[int index] => ref dense[index];

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return dense[i];
        }
    }

    public SparseSet(int initialCapacity = 16)
    {
        sparse = new Dictionary<T, int>(initialCapacity);
        dense = new T[Math.Max(initialCapacity, 16)];
    }


    public void Add(T key)
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

    public void Remove(T key)
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


    public bool Contains(T key)
    {
        return sparse.ContainsKey(key);
    }

    public int IndexOf(T key)
    {
        return sparse.TryGetValue(key, out int index) ? index : -1;
    }


    public void Clear()
    {
        sparse.Clear();
        count = 0;
    }
}
