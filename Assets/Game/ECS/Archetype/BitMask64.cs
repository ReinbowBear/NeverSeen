using System.Collections.Generic;

public struct BitMask64
{
    private ulong bits;

    public bool Has(int index) => (bits & (1UL << index)) != 0;
    public void Add(int index) => bits |= 1UL << index;
    public void Remove(int index) => bits &= ~(1UL << index);

    public bool MatchesAll(BitMask64 other) => (bits & other.bits) == other.bits;
    public bool MatchesAny(BitMask64 other) => (bits & other.bits) != 0;

    public bool IsZero() => bits == 0;
    public void Clear() => bits = 0;

    public IEnumerable<int> GetSetBits()
    {
        ulong temp = bits;
        int baseIndex = 0;

        while (temp != 0)
        {
            int bit = TrailingZeroCount(temp);
            yield return baseIndex + bit;
            temp &= temp - 1;
        }
    }

    public BitMask64 Clone() => new BitMask64 { bits = bits };

    public override int GetHashCode() => bits.GetHashCode();
    public override bool Equals(object obj) => obj is BitMask64 m && m.bits == bits;


    private int TrailingZeroCount(ulong x)
    {
        if (x == 0) return 64;
        int n = 0;
        while ((x & 1) == 0) { x >>= 1; n++; }
        return n;
    }
}
