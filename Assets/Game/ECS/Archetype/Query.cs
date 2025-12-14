
public readonly struct Query<T1> where T1 : struct, IComponentData
{
    private readonly QueryExecutor executor;

    public Query(QueryExecutor executor)
    {
        this.executor = executor;
    }


    public QueryEnumerator<T1> GetEnumerator()
    {
        return new QueryEnumerator<T1>(executor.Entries);
    }
}


public readonly struct Query<T1, T2> where T1 : struct, IComponentData where T2 : struct, IComponentData
{
    private readonly QueryExecutor executor;

    public Query(QueryExecutor executor)
    {
        this.executor = executor;
    }


    public QueryEnumerator<T1, T2> GetEnumerator()
    {
        return new QueryEnumerator<T1, T2>(executor.Entries);
    }
}


public readonly struct Query<T1, T2, T3> where T1 : struct, IComponentData where T2 : struct, IComponentData where T3 : struct, IComponentData
{
    private readonly QueryExecutor executor;

    public Query(QueryExecutor executor)
    {
        this.executor = executor;
    }


    public QueryEnumerator<T1, T2, T3> GetEnumerator()
    {
        return new QueryEnumerator<T1, T2, T3>(executor.Entries);
    }
}
