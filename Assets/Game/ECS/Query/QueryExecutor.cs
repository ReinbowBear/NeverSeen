
#region T1
using System;

public class QueryExecutor<T1> : IQueryExecutor
{
    private QueryDescription desc;
    public SparseSet<Entity> Entities = new();

    public Chunk<T1> Chunk1;

    public QueryExecutor(QueryDescription desc)
    {
        this.desc = desc;
    }


    public void TryAddEntity(Entity entity)
    {
        if (!entity.Mask.MatchesAll(desc.RequiredMask)) return;
        if (entity.Mask.MatchesAny(desc.ExcludedMask)) return;

        Entities.Add(entity);
    }

    public void TryRemoveEntity(Entity entity)
    {
        if (!entity.Mask.MatchesAll(desc.RequiredMask)) return;

        Entities.Remove(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        Entities.Remove(entity);
    }


    public QueryEnumerator<T1> GetEnumerator()
    {
        return new QueryEnumerator<T1>(this);
    }
}
#endregion

#region T2
public class QueryExecutor<T1, T2> : IQueryExecutor
{
    private QueryDescription desc;
    public SparseSet<Entity> Entities = new();

    public Chunk<T1> Chunk1;
    public Chunk<T2> Chunk2;

    public QueryExecutor(QueryDescription desc)
    {
        this.desc = desc;
    }


    public void TryAddEntity(Entity entity)
    {
        if (!entity.Mask.MatchesAll(desc.RequiredMask)) return;
        if (entity.Mask.MatchesAny(desc.ExcludedMask)) return;

        Entities.Add(entity);
    }

    public void TryRemoveEntity(Entity entity)
    {
        if (!entity.Mask.MatchesAll(desc.RequiredMask)) return;

        Entities.Remove(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        Entities.Remove(entity);
    }


    public QueryEnumerator<T1, T2> GetEnumerator()
    {
        return new QueryEnumerator<T1, T2>(this);
    }
}
#endregion

#region T3
public class QueryExecutor<T1, T2, T3> : IQueryExecutor
{
    private QueryDescription desc;
    public SparseSet<Entity> Entities = new();

    public Chunk<T1> Chunk1;
    public Chunk<T2> Chunk2;
    public Chunk<T3> Chunk3;

    public QueryExecutor(QueryDescription desc)
    {
        this.desc = desc;
    }


    public void TryAddEntity(Entity entity)
    {
        if (!entity.Mask.MatchesAll(desc.RequiredMask)) return;
        if (entity.Mask.MatchesAny(desc.ExcludedMask)) return;

        Entities.Add(entity);
    }

    public void TryRemoveEntity(Entity entity)
    {
        if (!entity.Mask.MatchesAll(desc.RequiredMask)) return;

        Entities.Remove(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        Entities.Remove(entity);
    }


    public QueryEnumerator<T1, T2, T3> GetEnumerator()
    {
        return new QueryEnumerator<T1, T2, T3>(this);
    }
}
#endregion
