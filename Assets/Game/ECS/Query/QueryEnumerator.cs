
#region T1
public struct QueryEnumerator<T1>
{
    private QueryExecutor<T1> executor;
    public Entity CurrentEntity => executor.Entities[index];
    private int index;

    public QueryEnumerator(QueryExecutor<T1> executor)
    {
        this.executor = executor;
        index = -1;
    }


    public bool MoveNext()
    {
        index++;
        return index < executor.Entities.Count;
    }

    public T1 Current
    {
        get
        {
            var entity = executor.Entities[index];
            var c1 = executor.Chunk1.GetComponent(entity);
            return c1;
        }
    }
}
#endregion

#region T2
public struct QueryEnumerator<T1, T2>
{
    private QueryExecutor<T1, T2> executor;
    public Entity CurrentEntity => executor.Entities[index];
    private int index;

    public QueryEnumerator(QueryExecutor<T1, T2> executor)
    {
        this.executor = executor;
        index = -1;
    }


    public bool MoveNext()
    {
        index++;
        return index < executor.Entities.Count;
    }


    public QueryTuple<T1, T2> Current
    {
        get
        {
            var entity = executor.Entities[index];            
            var c1 = executor.Chunk1.GetComponent(entity);
            var c2 = executor.Chunk2.GetComponent(entity);

            return new QueryTuple<T1, T2>(c1, c2);
        }
    }
}
#endregion

#region T3
public struct QueryEnumerator<T1, T2, Т3>
{
    private QueryExecutor<T1, T2, Т3> executor;
    public Entity CurrentEntity => executor.Entities[index];
    private int index;

    public QueryEnumerator(QueryExecutor<T1, T2, Т3> executor)
    {
        this.executor = executor;
        index = -1;
    }

    public bool MoveNext()
    {
        index++;
        return index < executor.Entities.Count;
    }

    public QueryTuple<T1, T2, Т3> Current
    {
        get
        {
            var entity = executor.Entities[index];
            var c1 = executor.Chunk1.GetComponent(entity);
            var c2 = executor.Chunk2.GetComponent(entity);
            var c3 = executor.Chunk3.GetComponent(entity);

            return new QueryTuple<T1, T2, Т3>(c1, c2, c3);
        }
    }
}
#endregion

#region T4
public struct QueryEnumerator<T1, T2, Т3, T4>
{
    private QueryExecutor<T1, T2, Т3, T4> executor;
    public Entity CurrentEntity => executor.Entities[index];
    private int index;

    public QueryEnumerator(QueryExecutor<T1, T2, Т3, T4> executor)
    {
        this.executor = executor;
        index = -1;
    }

    public bool MoveNext()
    {
        index++;
        return index < executor.Entities.Count;
    }

    public QueryTuple<T1, T2, Т3, T4> Current
    {
        get
        {
            var entity = executor.Entities[index];
            var c1 = executor.Chunk1.GetComponent(entity);
            var c2 = executor.Chunk2.GetComponent(entity);
            var c3 = executor.Chunk3.GetComponent(entity);
            var c4 = executor.Chunk4.GetComponent(entity);

            return new QueryTuple<T1, T2, Т3, T4>(c1, c2, c3, c4);
        }
    }
}
#endregion
