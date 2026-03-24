using System;
using System.Collections.Generic;

public class QueryRegistry
{
    private ComponentRegistry componentRegistry;
    private Dictionary<QueryKey, IQueryExecutor> cache = new();

    public QueryRegistry(ComponentRegistry componentRegistry)
    {
        this.componentRegistry = componentRegistry;
    }


    #region GetQueryExecutor
    public QueryExecutor<T1> GetQueryExecutor<T1>(QueryDescription desc)
    {
        var key = new QueryKey(desc);
        if (cache.TryGetValue(key, out var executor)) return (QueryExecutor<T1>)executor;

        var newExecutor = new QueryExecutor<T1>(desc)
        {
            Chunk1 = componentRegistry.GetStore<T1>()
        };

        cache.Add(key, newExecutor);
        return newExecutor;
    }

    public QueryExecutor<T1, T2> GetQueryExecutor<T1, T2>(QueryDescription desc)
    {
        var key = new QueryKey(desc);
        if (cache.TryGetValue(key, out var executor)) return (QueryExecutor<T1, T2>)executor;

        var newExecutor = new QueryExecutor<T1, T2>(desc)
        {
            Chunk1 = componentRegistry.GetStore<T1>(),
            Chunk2 = componentRegistry.GetStore<T2>(),
        };

        cache.Add(key, newExecutor);
        return newExecutor;
    }

    public QueryExecutor<T1, T2, T3> GetQueryExecutor<T1, T2, T3>(QueryDescription desc)
    {
        var key = new QueryKey(desc);
        if (cache.TryGetValue(key, out var executor)) return (QueryExecutor<T1, T2, T3>)executor;

        var newExecutor = new QueryExecutor<T1, T2, T3>(desc)
        {
            Chunk1 = componentRegistry.GetStore<T1>(),
            Chunk2 = componentRegistry.GetStore<T2>(),
            Chunk3 = componentRegistry.GetStore<T3>()
        };

        cache.Add(key, newExecutor);
        return newExecutor;
    }
    #endregion

    #region AddEntity
    public void TryAddEntity(Entity entity)
    {
        foreach (var executor in cache.Values)
        {
            executor.TryAddEntity(entity);
        }
    }

    public void TryRemoveEntity(Entity entity)
    {
        foreach (var executor in cache.Values)
        {
            executor.TryRemoveEntity(entity);
        }
    }

    public void RemoveEntity(Entity entity)
    {
        foreach (var executor in cache.Values)
        {
            executor.RemoveEntity(entity);
        }
    }
    #endregion
}

#region Key
public readonly struct QueryKey : IEquatable<QueryKey>
{
    private readonly BitMask64 required;
    private readonly BitMask64 changed;
    private readonly BitMask64 excluded;

    public QueryKey(QueryDescription desc)
    {
        required = desc.RequiredMask;
        changed = desc.ChangedMask;
        excluded = desc.ExcludedMask;
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

public interface IQueryExecutor
{
    void TryAddEntity(Entity entity);
    void TryRemoveEntity(Entity entity);
    void RemoveEntity(Entity entity);
}
#endregion
