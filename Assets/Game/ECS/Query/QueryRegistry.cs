using System.Collections.Generic;
using UnityEngine;

public class QueryRegistry
{
    private ComponentRegistry componentRegistry;
    private Dictionary<QueryDescription, IQueryExecutor> cache = new();

    public QueryRegistry(ComponentRegistry componentRegistry)
    {
        this.componentRegistry = componentRegistry;
    }


    #region GetQueryExecutor
    public QueryExecutor<T1> GetQueryExecutor<T1>(QueryDescription desc)
    {
        if (cache.TryGetValue(desc, out var executor)) return (QueryExecutor<T1>)executor;

        var newExecutor = new QueryExecutor<T1>(desc)
        {
            Chunk1 = componentRegistry.GetStore<T1>()
        };

        cache.Add(desc, newExecutor);
        return newExecutor;
    }

    public QueryExecutor<T1, T2> GetQueryExecutor<T1, T2>(QueryDescription desc)
    {
        if (cache.TryGetValue(desc, out var executor)) return (QueryExecutor<T1, T2>)executor;

        var newExecutor = new QueryExecutor<T1, T2>(desc)
        {
            Chunk1 = componentRegistry.GetStore<T1>(),
            Chunk2 = componentRegistry.GetStore<T2>(),
        };

        cache.Add(desc, newExecutor);
        return newExecutor;
    }

    public QueryExecutor<T1, T2, T3> GetQueryExecutor<T1, T2, T3>(QueryDescription desc)
    {
        if (cache.TryGetValue(desc, out var executor)) return (QueryExecutor<T1, T2, T3>)executor;

        var newExecutor = new QueryExecutor<T1, T2, T3>(desc)
        {
            Chunk1 = componentRegistry.GetStore<T1>(),
            Chunk2 = componentRegistry.GetStore<T2>(),
            Chunk3 = componentRegistry.GetStore<T3>()
        };

        cache.Add(desc, newExecutor);
        return newExecutor;
    }

    public QueryExecutor<T1, T2, T3, T4> GetQueryExecutor<T1, T2, T3, T4>(QueryDescription desc)
    {
        if (cache.TryGetValue(desc, out var executor)) return (QueryExecutor<T1, T2, T3, T4>)executor;

        var newExecutor = new QueryExecutor<T1, T2, T3, T4>(desc)
        {
            Chunk1 = componentRegistry.GetStore<T1>(),
            Chunk2 = componentRegistry.GetStore<T2>(),
            Chunk3 = componentRegistry.GetStore<T3>(),
            Chunk4 = componentRegistry.GetStore<T4>()
        };

        cache.Add(desc, newExecutor);
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


    public void Clear()
    {
        cache.Clear();
    }
}

public interface IQueryExecutor
{
    void TryAddEntity(Entity entity);
    void TryRemoveEntity(Entity entity);
    void RemoveEntity(Entity entity);
}
