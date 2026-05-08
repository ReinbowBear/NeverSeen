
#region T1
public struct Query<T1>
{
    public TypeRegistry TypeRegistry;
    public MaskRegistry MaskRegistry;
    public QueryMask Masc;

    public ComponentStore<T1> Store1;


    public Query<T1> Require<T>()
    {
        var index = TypeRegistry.GetIndex(typeof(T));
        Masc.RequiredMask.Add(index);
        return this;
    }

    public Query<T1> Exclude<T>()
    {
        var index = TypeRegistry.GetIndex(typeof(T));
        Masc.ExcludedMask.Add(index);
        return this;
    }


    public QueryEnumerator<T1> GetEnumerator()
    {
        return new QueryEnumerator<T1>(this);
    }
}
#endregion


#region T2
public struct Query<T1, T2>
{
    public TypeRegistry TypeRegistry;
    public MaskRegistry MaskRegistry;
    public QueryMask Masc;

    public ComponentStore<T1> Store1;
    public ComponentStore<T2> Store2;


    public Query<T1, T2> Require<T>()
    {
        var index = TypeRegistry.GetIndex(typeof(T));
        Masc.RequiredMask.Add(index);
        return this;
    }

    public Query<T1, T2> Exclude<T>()
    {
        var index = TypeRegistry.GetIndex(typeof(T));
        Masc.ExcludedMask.Add(index);
        return this;
    }


    public QueryEnumerator<T1, T2> GetEnumerator()
    {
        return new QueryEnumerator<T1, T2>(this);
    }
}
#endregion


#region T3
public struct Query<T1, T2, T3>
{
    public TypeRegistry TypeRegistry;
    public MaskRegistry MaskRegistry;
    public QueryMask Masc;

    public ComponentStore<T1> Store1;
    public ComponentStore<T2> Store2;
    public ComponentStore<T3> Store3;


    public Query<T1, T2, T3> Require<T>()
    {
        var index = TypeRegistry.GetIndex(typeof(T));
        Masc.RequiredMask.Add(index);
        return this;
    }

    public Query<T1, T2, T3> Exclude<T>()
    {
        var index = TypeRegistry.GetIndex(typeof(T));
        Masc.ExcludedMask.Add(index);
        return this;
    }


    public QueryEnumerator<T1, T2, T3> GetEnumerator()
    {
        return new QueryEnumerator<T1, T2, T3>(this);
    }
}
#endregion

#region T4
public struct Query<T1, T2, T3, T4>
{
    public TypeRegistry TypeRegistry;
    public MaskRegistry MaskRegistry;
    public QueryMask Masc;

    public ComponentStore<T1> Store1;
    public ComponentStore<T2> Store2;
    public ComponentStore<T3> Store3;
    public ComponentStore<T4> Store4;


    public Query<T1, T2, T3, T4> Require<T>()
    {
        var index = TypeRegistry.GetIndex(typeof(T));
        Masc.RequiredMask.Add(index);
        return this;
    }

    public Query<T1, T2, T3, T4> Exclude<T>()
    {
        var index = TypeRegistry.GetIndex(typeof(T));
        Masc.ExcludedMask.Add(index);
        return this;
    }


    public QueryEnumerator<T1, T2, T3, T4> GetEnumerator()
    {
        return new QueryEnumerator<T1, T2, T3, T4>(this);
    }
}
#endregion
