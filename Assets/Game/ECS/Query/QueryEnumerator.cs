
#region T1
public struct QueryEnumerator<T1>
{
    private QueryExecutor<T1> executor;
    private Entity currentEntity;
    private int index;

    public QueryEnumerator(QueryExecutor<T1> executor)
    {
        this.executor = executor;
        this.currentEntity = default;
        this.index = -1;
    }


    public bool MoveNext()
    {
        index++;

        if (index >= executor.Entities.Count) return false;

        currentEntity = executor.Entities[index];
        return true;
    }

    public ComponentProxy<T1> Current
    {
        get
        {
            return new ComponentProxy<T1>(executor.Chunk1, ref currentEntity);
        }
    }
}
#endregion

#region T2
public struct QueryEnumerator<T1, T2>
{
    private QueryExecutor<T1, T2> executor;
    private Entity currentEntity;
    private int index;

    public QueryEnumerator(QueryExecutor<T1, T2> executor)
    {
        this.executor = executor;
        this.currentEntity = default;
        this.index = -1;
    }


    public bool MoveNext()
    {
        index++;

        if (index >= executor.Entities.Count) return false;

        currentEntity = executor.Entities[index];
        return true;
    }

    public QueryTuple<T1, T2> Current
    {
        get
        {
            var p1 = new ComponentProxy<T1>(executor.Chunk1, ref currentEntity);
            var p2 = new ComponentProxy<T2>(executor.Chunk2, ref currentEntity);

            return new QueryTuple<T1, T2>(p1, p2);
        }
    }
}
#endregion

#region T3
public struct QueryEnumerator<T1, T2, T3>
{
    private QueryExecutor<T1, T2, T3> executor;
    private Entity currentEntity;
    private int index;

    public QueryEnumerator(QueryExecutor<T1, T2, T3> executor)
    {
        this.executor = executor;
        this.currentEntity = default;
        this.index = -1;
    }


    public bool MoveNext()
    {
        index++;

        if (index >= executor.Entities.Count) return false;

        currentEntity = executor.Entities[index];
        return true;
    }

    public QueryTuple<T1, T2, T3> Current
    {
        get
        {
            var p1 = new ComponentProxy<T1>(executor.Chunk1, ref currentEntity);
            var p2 = new ComponentProxy<T2>(executor.Chunk2, ref currentEntity);
            var p3 = new ComponentProxy<T3>(executor.Chunk3, ref currentEntity);

            return new QueryTuple<T1, T2, T3>(p1, p2, p3);
        }
    }
}
#endregion
