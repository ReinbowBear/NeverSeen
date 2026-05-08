using System;
using System.Collections.Generic;

public readonly struct StructKey<T1, T2> : IEquatable<StructKey<T1, T2>> // ключи учитывают порядок элементов!
{
    public readonly T1 Item1;
    public readonly T2 Item2;

    public StructKey(T1 T1, T2 T2)
    {
        Item1 = T1;
        Item2 = T2;
    }


    public bool Equals(StructKey<T1, T2> other)
    {
        return EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
            && EqualityComparer<T2>.Default.Equals(Item2, other.Item2);
    }

    public override bool Equals(object obj)
    {
        return obj is StructKey<T1, T2> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Item1, Item2);
    }

    public override string ToString()
    {
        return $"({Item1}, {Item2})";
    }
}


public readonly struct StructKey<T1, T2, T3> : IEquatable<StructKey<T1, T2, T3>>
{
    public readonly T1 Item1;
    public readonly T2 Item2;
    public readonly T3 Item3;

    public StructKey(T1 T1, T2 T2, T3 T3)
    {
        Item1 = T1;
        Item2 = T2;
        Item3 = T3;
    }


    public bool Equals(StructKey<T1, T2, T3> other)
    {
        return EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
            && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
            && EqualityComparer<T3>.Default.Equals(Item3, other.Item3);
    }

    public override bool Equals(object obj)
    {
        return obj is StructKey<T1, T2, T3> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Item1, Item2, Item3);
    }

    public override string ToString()
    {
        return $"({Item1}, {Item2}, {Item3})";
    }
}
