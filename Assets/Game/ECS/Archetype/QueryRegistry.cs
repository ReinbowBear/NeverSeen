using System;
using System.Collections.Generic;

public sealed class QueryRegistry
{
    private readonly Dictionary<QueryKey, QueryExecutor> cache = new();
    private readonly ArchetypeRegistry archetypeRegistry;

    public QueryRegistry(ArchetypeRegistry archetypeRegistry)
    {
        this.archetypeRegistry = archetypeRegistry;
    }


    public QueryExecutor GetQuery(QueryDescription desc)
    {
        var key = new QueryKey(desc);

        if (cache.TryGetValue(key, out var executor)) return executor;

        executor = new QueryExecutor(archetypeRegistry, desc);
        cache.Add(key, executor);
        return executor;
    }


    public void AddArchetype(Archetype archetype)
    {
        foreach (var executor in cache.Values)
        {
            executor.AddArchetype(archetype);
        }
    }
}


public readonly struct QueryKey : IEquatable<QueryKey>
{
    private readonly BitMask64 required;
    private readonly BitMask64 changed;
    private readonly BitMask64 excluded;

    public QueryKey(QueryDescription desc)
    {
        required = desc.RequiredMask.Clone();
        changed = desc.ChangedMask.Clone();
        excluded = desc.ExcludedMask.Clone();
    }


    public bool Equals(QueryKey other)
    {
        return required.Equals(other.required) && changed.Equals(other.changed) && excluded.Equals(other.excluded);
    }

    public override bool Equals(object obj) => obj is QueryKey other && Equals(other);

    public override int GetHashCode()
    {
        int hash = required.GetHashCode();
        hash = (hash * 397) ^ changed.GetHashCode();
        hash = (hash * 397) ^ excluded.GetHashCode();
        return hash;
    }
}
