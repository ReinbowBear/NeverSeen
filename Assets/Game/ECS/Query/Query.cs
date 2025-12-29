
#region T1
public readonly struct Query<T1> where T1 : struct, IComponentData
{
    private readonly QueryRegistry queryRegistry;
    private readonly QueryDescription desc;

    public Query(QueryRegistry queryRegistry)
    {
        this.queryRegistry = queryRegistry;
        this.desc = new();
    }


    public Query<T1> Require<T>() where T : struct, IComponentData
    {
        desc.Require<T>();
        return this;
    }

    public Query<T1> Exclude<T>() where T : struct, IComponentData
    {
        desc.Exclude<T>();
        return this;
    }

    public Query<T1> Changed<T>() where T : struct, IComponentData
    {
        desc.Changed<T>();
        return this;
    }


    public QueryEnumerator<T1> GetEnumerator()
    {
        var executor = queryRegistry.GetQuery(desc);
        return new QueryEnumerator<T1>(executor.Entries);
    }
}
#endregion

#region T2
public readonly struct Query<T1, T2> where T1 : struct, IComponentData where T2 : struct, IComponentData
{
    private readonly QueryRegistry queryRegistry;
    private readonly QueryDescription desc;

    public Query(QueryRegistry queryRegistry)
    {
        this.queryRegistry = queryRegistry;
        this.desc = new();
    }


    public Query<T1, T2> Require<T>() where T : struct, IComponentData
    {
        desc.Require<T>();
        return this;
    }

    public Query<T1, T2> Exclude<T>() where T : struct, IComponentData
    {
        desc.Exclude<T>();
        return this;
    }

    public Query<T1, T2> Changed<T>() where T : struct, IComponentData
    {
        desc.Changed<T>();
        return this;
    }


    public QueryEnumerator<T1, T2> GetEnumerator()
    {
        var executor = queryRegistry.GetQuery(desc);
        return new QueryEnumerator<T1, T2>(executor.Entries);
    }
}
#endregion

#region T3
public readonly struct Query<T1, T2, T3> where T1 : struct, IComponentData where T2 : struct, IComponentData where T3 : struct, IComponentData
{
    private readonly QueryRegistry queryRegistry;
    private readonly QueryDescription desc;

    public Query(QueryRegistry queryRegistry)
    {
        this.queryRegistry = queryRegistry;
        this.desc = new();
    }


    public Query<T1, T2, T3> Require<T>() where T : struct, IComponentData
    {
        desc.Require<T>();
        return this;
    }

    public Query<T1, T2, T3> Exclude<T>() where T : struct, IComponentData
    {
        desc.Exclude<T>();
        return this;
    }

    public Query<T1, T2, T3> Changed<T>() where T : struct, IComponentData
    {
        desc.Changed<T>();
        return this;
    }


    public QueryEnumerator<T1, T2, T3> GetEnumerator()
    {
        var executor = queryRegistry.GetQuery(desc);
        return new QueryEnumerator<T1, T2, T3>(executor.Entries);
    }
}
#endregion
