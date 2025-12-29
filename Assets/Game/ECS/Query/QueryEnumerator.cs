using System.Collections.Generic;

#region T1
public ref struct QueryEnumerator<T1> where T1 : struct, IComponentData
{
    private readonly List<ArchetypeEntry> entries;
    private int entryIndex;
    private int chunkIndex;
    private int slotIndex;

    private Chunk currentChunk;
    private ArchetypeEntry currentEntry;

    public QueryEnumerator(List<ArchetypeEntry> entries)
    {
        this.entries = entries;
        entryIndex = -1;
        chunkIndex = 0;
        slotIndex = 0;
        currentChunk = null;
        currentEntry = null;
    }


    public bool MoveNext()
    {
        while (true)
        {
            //if (currentChunk != null && ++slotIndex < currentChunk.Entities.Count)
            //{
            //    if (desc.ChangedMask.IsEmpty || currentChunk.WasChanged(slotIndex, desc.ChangedMask)) return true;
            //    continue;
            //}
            if (currentChunk != null && ++slotIndex < currentChunk.Entities.Count) return true; // тут увеличение индекса в проверке

            while (currentEntry != null && chunkIndex < currentEntry.Archetype.Chunks.Count)
            {
                currentChunk = currentEntry.Archetype.Chunks[chunkIndex++];
                slotIndex = -1;
                break;
            }

            if (currentChunk != null) continue;
            if (++entryIndex >= entries.Count) return false; // и тут тоже

            currentEntry = entries[entryIndex];
            chunkIndex = 0;
        }
    }

    public QueryTuple<T1> Current
    {
        get
        {
            var compIndex1 = currentEntry.LocalIndices[0]; // индекс соотведствует первому компоненту запроса

            return new QueryTuple<T1>
            (
                new ComponentProxy<T1>(currentChunk, slotIndex, compIndex1)
            );
        }
    }
}
#endregion

#region T2
public ref struct QueryEnumerator<T1, T2> where T1 : struct, IComponentData where T2 : struct, IComponentData
{
    private readonly List<ArchetypeEntry> entries;
    private int entryIndex;
    private int chunkIndex;
    private int slotIndex;

    private Chunk currentChunk;
    private ArchetypeEntry currentEntry;

    public QueryEnumerator(List<ArchetypeEntry> entries)
    {
        this.entries = entries;
        entryIndex = -1;
        chunkIndex = 0;
        slotIndex = 0;
        currentChunk = null;
        currentEntry = null;
    }


    public bool MoveNext()
    {
        while (true)
        {
            if (currentChunk != null && ++slotIndex < currentChunk.Entities.Count) return true;

            while (currentEntry != null && chunkIndex < currentEntry.Archetype.Chunks.Count)
            {
                currentChunk = currentEntry.Archetype.Chunks[chunkIndex++];
                slotIndex = -1;
                break;
            }

            if (currentChunk != null) continue;
            if (++entryIndex >= entries.Count) return false;

            currentEntry = entries[entryIndex];
            chunkIndex = 0;
        }
    }

    public QueryTuple<T1, T2> Current
    {
        get
        {
            var compIndex1 = currentEntry.LocalIndices[0];
            var compIndex2 = currentEntry.LocalIndices[1];

            return new QueryTuple<T1, T2>
            (
                new ComponentProxy<T1>(currentChunk, slotIndex, compIndex1),
                new ComponentProxy<T2>(currentChunk, slotIndex, compIndex2)
            );
        }
    }
}
#endregion

#region T3
public ref struct QueryEnumerator<T1, T2, T3> where T1 : struct, IComponentData where T2 : struct, IComponentData where T3 : struct, IComponentData
{
    private readonly List<ArchetypeEntry> entries;
    private int entryIndex;
    private int chunkIndex;
    private int slotIndex;

    private Chunk currentChunk;
    private ArchetypeEntry currentEntry;

    public QueryEnumerator(List<ArchetypeEntry> entries)
    {
        this.entries = entries;
        entryIndex = -1;
        chunkIndex = 0;
        slotIndex = 0;
        currentChunk = null;
        currentEntry = null;
    }

    
    public bool MoveNext()
    {
        while (true)
        {
            if (currentChunk != null && ++slotIndex < currentChunk.Entities.Count) return true;

            while (currentEntry != null && chunkIndex < currentEntry.Archetype.Chunks.Count)
            {
                currentChunk = currentEntry.Archetype.Chunks[chunkIndex++];
                slotIndex = -1;
                break;
            }

            if (currentChunk != null) continue;
            if (++entryIndex >= entries.Count) return false;

            currentEntry = entries[entryIndex];
            chunkIndex = 0;
        }
    }

    public QueryTuple<T1, T2, T3> Current
    {
        get
        {
            var compIndex1 = currentEntry.LocalIndices[0];
            var compIndex2 = currentEntry.LocalIndices[1];
            var compIndex3 = currentEntry.LocalIndices[2];

            return new QueryTuple<T1, T2, T3>
            (
                new ComponentProxy<T1>(currentChunk, slotIndex, compIndex1),
                new ComponentProxy<T2>(currentChunk, slotIndex, compIndex2),
                new ComponentProxy<T3>(currentChunk, slotIndex, compIndex2)
            );
        }
    }
}
#endregion
