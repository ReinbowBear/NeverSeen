using System;
using System.Collections.Generic;

public class BitMaskSet
{
    private readonly List<BitMask64> masks = new();

    public BitMaskSet(int capacity)
    {
        int count = (capacity + 63) / 64;

        for (int i = 0; i < count; i++)
        {
            masks.Add(new BitMask64());
        }
    }

    private (int maskIndex, int bitIndex) GetIndices(int index) => (index / 64, index % 64);

    public bool Has(int index)
    {
        var (mi, bi) = GetIndices(index);
        return masks[mi].Has(bi);
    }

    public void Add(int index)
    {
        var (mi, bi) = GetIndices(index);
        masks[mi].Add(bi);
    }

    public void Remove(int index)
    {
        var (mi, bi) = GetIndices(index);
        masks[mi].Remove(bi);
    }

    public bool MatchesAll(BitMaskSet other)
    {
        int min = Math.Min(masks.Count, other.masks.Count);

        for (int i = 0; i < min; i++)
        {
            if (!masks[i].MatchesAll(other.masks[i]))return false;
        }

        for (int i = min; i < other.masks.Count; i++)
        {
            if (!other.masks[i].IsZero())return false;
        }

        return true;
    }

    public bool MatchesAny(BitMaskSet other)
    {
        int min = Math.Min(masks.Count, other.masks.Count);

        for (int i = 0; i < min; i++)
        {
            if (masks[i].MatchesAny(other.masks[i]))return true;
        }

        return false;
    }

    public bool IsZero()
    {
        foreach (var mask in masks)
        {
            if (!mask.IsZero()) return false;
        }
        return true;
    }

    public void Clear()
    {
        foreach (var mask in masks) mask.Clear();

    }
}
