
public struct BitMask
{
    private ulong bits;

    public bool Get(int index) => (bits & (1UL << index)) != 0;
    public void Set(int index, bool value)
    {
        if (value) bits |= 1UL << index;
        else bits &= ~(1UL << index);
    }

    public bool Matches(BitMask other) => (bits & other.bits) == other.bits;
}
