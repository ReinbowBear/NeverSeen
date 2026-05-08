using UnityEngine;

#region T1
public ref struct QueryEnumerator<T1>
{
    private Entity[] entities;
    private int count;
    private int index;

    private Query<T1> query;
    public QueryTuple<T1> Current { get; private set; }

    public QueryEnumerator(Query<T1> query)
    {
        this.query = query;

        entities = query.Store1.Entities;
        count = query.Store1.Count;

        index = -1;
        Current = default;
    }
    
    public bool MoveNext()
    {
        var store = query.Store1;
        ref var required = ref query.Masc.RequiredMask;
        ref var excluded = ref query.Masc.ExcludedMask;

        while (++index < count)
        {
            var entity = entities[index];
            ref var mask = ref query.MaskRegistry.GetMask(entity.Id);
    
            if (!mask.MatchesAll(required)) continue;
            if (mask.MatchesAny(excluded)) continue;

            int id = entity.Id;
            int idx = store.Sparse[id];

            if (idx == -1) continue;

            Current = new QueryTuple<T1>
            (
                store.Components[idx],
                entity
            );
            return true;
        }

        return false;
    }
}
#endregion


#region T2
public ref struct QueryEnumerator<T1, T2>
{
    private Entity[] entities;
    private int count;
    private int index;

    private Query<T1, T2> query;
    public QueryTuple<T1, T2> Current { get; private set; }

    public QueryEnumerator(Query<T1, T2> query)
    {
        this.query = query;

        int c1 = query.Store1.Count;
        int c2 = query.Store2.Count;

        entities = query.Store1.Entities;
        int min = c1;

        if (c2 < min) { min = c2; entities = query.Store2.Entities; }

        count = min;
        index = -1;
        Current = default;
    }


    public bool MoveNext()
    {
        var s1 = query.Store1;
        var s2 = query.Store2;

        ref var required = ref query.Masc.RequiredMask;
        ref var excluded = ref query.Masc.ExcludedMask;

        while (++index < count)
        {
            var entity = entities[index];
            ref var mask = ref query.MaskRegistry.GetMask(entity.Id);
    
            if (!mask.MatchesAll(required)) continue;
            if (mask.MatchesAny(excluded)) continue;

            int id = entity.Id;

            int i1 = s1.Sparse[id];
            int i2 = s2.Sparse[id];

            if (i1 == -1 || i2 == -1) continue;

            Current = new QueryTuple<T1, T2>
            (
                s1.Components[i1],
                s2.Components[i2],
                entity
            );

            return true;
        }

        return false;
    }
}
#endregion


#region T3
public ref struct QueryEnumerator<T1, T2, T3>
{
    private Entity[] entities;
    private int count;
    private int index;

    private Query<T1, T2, T3> query;
    public QueryTuple<T1, T2, T3> Current { get; private set; }

    public QueryEnumerator(Query<T1, T2, T3> query)
    {
        this.query = query;

        int c1 = query.Store1.Count;
        int c2 = query.Store2.Count;
        int c3 = query.Store3.Count;

        entities = query.Store1.Entities;
        int min = c1;

        if (c2 < min) { min = c2; entities = query.Store2.Entities; }
        if (c3 < min) { min = c3; entities = query.Store3.Entities; }

        count = min;
        index = -1;
        Current = default;
    }

    public bool MoveNext()
    {
        var s1 = query.Store1;
        var s2 = query.Store2;
        var s3 = query.Store3;

        ref var required = ref query.Masc.RequiredMask;
        ref var excluded = ref query.Masc.ExcludedMask;

        while (++index < count)
        {
            var entity = entities[index];
            ref var mask = ref query.MaskRegistry.GetMask(entity.Id);
    
            if (!mask.MatchesAll(required)) continue;
            if (mask.MatchesAny(excluded)) continue;

            int id = entity.Id;

            int i1 = s1.Sparse[id];
            int i2 = s2.Sparse[id];
            int i3 = s3.Sparse[id];

            if (i1 == -1 || i2 == -1 || i3 == -1) continue;

            Current = new QueryTuple<T1, T2, T3>
            (
                s1.Components[i1],
                s2.Components[i2],
                s3.Components[i3],
                entity
            );

            return true;
        }

        return false;
    }
}
#endregion


#region T4
public ref struct QueryEnumerator<T1, T2, T3, T4>
{
    private Entity[] entities;
    private int count;
    private int index;

    private Query<T1, T2, T3, T4> query;
    public QueryTuple<T1, T2, T3, T4> Current { get; private set; }

    public QueryEnumerator(Query<T1, T2, T3, T4> query)
    {
        this.query = query;

        int c1 = query.Store1.Count;
        int c2 = query.Store2.Count;
        int c3 = query.Store3.Count;
        int c4 = query.Store4.Count;

        entities = query.Store1.Entities;
        int min = c1;

        if (c2 < min) { min = c2; entities = query.Store2.Entities; }
        if (c3 < min) { min = c3; entities = query.Store3.Entities; }
        if (c4 < min) { min = c4; entities = query.Store4.Entities; }

        count = min;
        index = -1;
        Current = default;
    }


    public bool MoveNext()
    {
        var s1 = query.Store1;
        var s2 = query.Store2;
        var s3 = query.Store3;
        var s4 = query.Store4;

        ref var required = ref query.Masc.RequiredMask;
        ref var excluded = ref query.Masc.ExcludedMask;

        while (++index < count)
        {
            var entity = entities[index];
            ref var mask = ref query.MaskRegistry.GetMask(entity.Id);
    
            if (!mask.MatchesAll(required)) continue;
            if (mask.MatchesAny(excluded)) continue;

            int id = entity.Id;

            int i1 = s1.Sparse[id];
            int i2 = s2.Sparse[id];
            int i3 = s3.Sparse[id];
            int i4 = s4.Sparse[id];

            if (i1 == -1 || i2 == -1 || i3 == -1 || i4 == -1) continue;

            Current = new QueryTuple<T1, T2, T3, T4>
            (
                s1.Components[i1],
                s2.Components[i2],
                s3.Components[i3],
                s4.Components[i4],
                entity
            );

            return true;
        }

        return false;
    }
}
#endregion
