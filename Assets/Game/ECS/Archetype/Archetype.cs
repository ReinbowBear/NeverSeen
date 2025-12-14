using System;
using System.Collections.Generic;
using System.Linq;

public sealed class Archetype
{
    public readonly BitMask64 Mask;
    public readonly List<Type> ComponentTypes;
    public readonly List<Chunk> Chunks = new();

    public readonly int[] componentToLocal;
    private Chunk lastUsedChunk;

    public Archetype(List<Type> types, BitMask64 mask)
    {
        Mask = mask;
        ComponentTypes = types;

        var bits = mask.GetSetBits().ToArray();
        int last = bits[^1];

        componentToLocal = new int[last + 1];
        Array.Fill(componentToLocal, -1);

        for (int local = 0; local < bits.Length; local++)
        {
            componentToLocal[bits[local]] = local;
        }
    }


    public Chunk GetChunk()
    {
        if (lastUsedChunk != null && lastUsedChunk.HasSpace) return lastUsedChunk;

        foreach (var chunk in Chunks)
        {
            if (chunk.HasSpace)
            {
                lastUsedChunk = chunk;
                return chunk;
            }
        }

        var newChunk = new Chunk(ComponentTypes);
        Chunks.Add(newChunk);

        lastUsedChunk = newChunk;
        return newChunk;
    }
}
