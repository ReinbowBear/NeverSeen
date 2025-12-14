using System.Collections.Generic;
using System.Linq;

public sealed class QueryExecutor
{
    private QueryDescription desc;
    public readonly List<ArchetypeEntry> Entries = new();


    public QueryExecutor(ArchetypeRegistry archRegistry, QueryDescription desc)
    {
        this.desc = desc;      

        foreach (var arch in archRegistry.GetAllArchetypes())
        {
            AddArchetype(arch);
        }
    }


    public void AddArchetype(Archetype arch)
    {
        if (!arch.Mask.MatchesAll(desc.RequiredMask)) return;
        if (arch.Mask.MatchesAny(desc.ExcludedMask)) return;

        var localIndices = ResolveLocalIndices(arch);
        Entries.Add(new ArchetypeEntry(arch, localIndices));
    }


    private int[] ResolveLocalIndices(Archetype arch)
    {
        var ids = desc.RequiredMask.GetSetBits().ToArray();
        var result = new int[ids.Length];

        for (int i = 0; i < ids.Length; i++)
        {
            result[i] = arch.componentToLocal[ids[i]];
        }

        return result;
    }
}


public sealed class ArchetypeEntry
{
    public readonly Archetype Archetype;
    public readonly int[] LocalIndices;

    public ArchetypeEntry(Archetype archetype, int[] localIndices)
    {
        Archetype = archetype;
        LocalIndices = localIndices;
    }
}
