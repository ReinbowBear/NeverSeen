
#region T1
public struct Query<T1>
{
    private QueryRegistry queryRegistry;
    private QueryDescription desc;

    public Query(QueryRegistry queryRegistry, QueryDescription desc)
    {
        this.queryRegistry = queryRegistry;
        this.desc = desc;
    }


    public Query<T1> Require<T>()
    {
        var newDesc = desc.Require<T>();
        desc = newDesc;
        return this;
    }

    public Query<T1> Exclude<T>()
    {
        var newDesc = desc.Exclude<T>();
        desc = newDesc;
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
public struct Query<T1, T2>
{
    private QueryRegistry queryRegistry;
    private QueryDescription desc;

    public Query(QueryRegistry queryRegistry, QueryDescription desc)
    {
        this.queryRegistry = queryRegistry;
        this.desc = desc;
    }


    public Query<T1, T2> Require<T>()
    {
        var newDesc = desc.Require<T>();
        desc = newDesc;
        return this;
    }

    public Query<T1, T2> Exclude<T>()
    {
        var newDesc = desc.Exclude<T>();
        desc = newDesc;
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
public struct Query<T1, T2, T3>
{
    private QueryRegistry queryRegistry;
    private QueryDescription desc;

    public Query(QueryRegistry queryRegistry, QueryDescription desc)
    {
        this.queryRegistry = queryRegistry;
        this.desc = desc;
    }


    public Query<T1, T2, T3> Require<T>()
    {
        var newDesc = desc.Require<T>();
        desc = newDesc;
        return this;
    }

    public Query<T1, T2, T3> Exclude<T>()
    {
        var newDesc = desc.Exclude<T>();
        desc = newDesc;
        return this;
    }


    public QueryEnumerator<T1, T2, T3> GetEnumerator()
    {
        var executor = queryRegistry.GetQueryExecutor<T1, T2, T3>(desc);
        return new QueryEnumerator<T1, T2, T3>(executor);
    }
}
#endregion

#region T4
public struct Query<T1, T2, T3, T4>
{
    private QueryRegistry queryRegistry;
    private QueryDescription desc;

    public Query(QueryRegistry queryRegistry, QueryDescription desc)
    {
        this.queryRegistry = queryRegistry;
        this.desc = desc;
    }


    public Query<T1, T2, T3, T4> Require<T>()
    {
        var newDesc = desc.Require<T>();
        desc = newDesc;
        return this;
    }

    public Query<T1, T2, T3, T4> Exclude<T>()
    {
        var newDesc = desc.Exclude<T>();
        desc = newDesc;
        return this;
    }


    public QueryEnumerator<T1, T2, T3, T4> GetEnumerator()
    {
        var executor = queryRegistry.GetQueryExecutor<T1, T2, T3, T4>(desc);
        return new QueryEnumerator<T1, T2, T3, T4>(executor);
    }
}
#endregion
