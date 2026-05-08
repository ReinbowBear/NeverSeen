using System;

public class MaskRegistry
{
    private BitMask64[] masks;

    public MaskRegistry(int initialCapacity = 64)
    {
        masks = new BitMask64[initialCapacity];
    }


    public ref BitMask64 GetMask(int entityId)
    {
        return ref masks[entityId];
    }


    public void EnsureCapacity(int entityId)
    {
        if (entityId < masks.Length) return;

        int newSize = Math.Max(masks.Length * 2, entityId + 1);
        Array.Resize(ref masks, newSize);
    }
}
