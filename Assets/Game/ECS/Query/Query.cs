
#region T1
public readonly struct Query<T1>
{
    private readonly QueryRegistry queryRegistry;
    private readonly QueryDescription desc;

    public Query(QueryRegistry queryRegistry, QueryDescription desc)
    {
        this.queryRegistry = queryRegistry;
        this.desc = desc;
    }


    public Query<T1> Require<T>()
    {
        desc.Require<T>();
        return this;
    }

    public Query<T1> Exclude<T>()
    {
        desc.Exclude<T>();
        return this;
    }

    public Query<T1> Changed<T>()
    {
        desc.Changed<T>();
        return this;
    }


    public QueryEnumerator<T1> GetEnumerator()
    {
        var executor = queryRegistry.GetQueryExecutor<T1>(desc);
        return new QueryEnumerator<T1>(executor);
    }
}
#endregion

#region T2
public readonly struct Query<T1, T2>
{
    private readonly QueryRegistry queryRegistry;
    private readonly QueryDescription desc;

    public Query(QueryRegistry queryRegistry, QueryDescription desc)
    {
        this.queryRegistry = queryRegistry;
        this.desc = desc;
    }


    public Query<T1, T2> Require<T>()
    {
        desc.Require<T>();
        return this;
    }

    public Query<T1, T2> Exclude<T>()
    {
        desc.Exclude<T>();
        return this;
    }

    public Query<T1, T2> Changed<T>()
    {
        desc.Changed<T>();
        return this;
    }


    public QueryEnumerator<T1, T2> GetEnumerator()
    {
        var executor = queryRegistry.GetQueryExecutor<T1, T2>(desc);
        return new QueryEnumerator<T1, T2>(executor);
    }
}
#endregion

#region T3
public readonly struct Query<T1, T2, T3>
{
    private readonly QueryRegistry queryRegistry;
    private readonly QueryDescription desc;

    public Query(QueryRegistry queryRegistry, QueryDescription desc)
    {
        this.queryRegistry = queryRegistry;
        this.desc = desc;
    }


    public Query<T1, T2, T3> Require<T>()
    {
        desc.Require<T>();
        return this;
    }

    public Query<T1, T2, T3> Exclude<T>()
    {
        desc.Exclude<T>();
        return this;
    }

    public Query<T1, T2, T3> Changed<T>()
    {
        desc.Changed<T>();
        return this;
    }


    public QueryEnumerator<T1, T2, T3> GetEnumerator()
    {
        var executor = queryRegistry.GetQueryExecutor<T1, T2, T3>(desc);
        return new QueryEnumerator<T1, T2, T3>(executor);
    }
}
#endregion
