using System;

public class SortRule<T>
{
    public Func<T, int> Selector;
    public bool Descending;

    public SortRule(Func<T, int> selector, bool descending = false)
    {
        Selector = selector;
        Descending = descending;
    }

    public int Compare(T a, T b)
    {
        int result = Selector(a).CompareTo(Selector(b));
        return Descending ? -result : result;
    }
}
