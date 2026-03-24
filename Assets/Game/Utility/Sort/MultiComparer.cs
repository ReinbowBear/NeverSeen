using System;
using System.Collections.Generic;

public class MultiComparer<T> : IComparer<T>
{
    private readonly List<SortRule<T>> rules = new();

    public void AddRule(Func<T, int> selector, bool descending = false)
    {
        rules.Add(new SortRule<T>(selector, descending));
    }

    public int Compare(T a, T b)
    {
        foreach (var rule in rules)
        {
            int result = rule.Compare(a, b);
            if (result != 0) return result;
        }

        return 0;
    }
}
