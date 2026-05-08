
#region T1
public readonly ref struct QueryTuple<T1>
{
    public readonly T1 Item1;
    public readonly Entity Entity;

    public QueryTuple(T1 item1, Entity entity)
    {
        Item1 = item1;
        Entity = entity;
    }

    public void Deconstruct(out T1 item1)
    {
        item1 = Item1;
    }

    public void Deconstruct(out T1 item1, out Entity entity)
    {
        item1 = Item1;
        entity = Entity;
    }
}
#endregion

#region T2
public readonly ref struct QueryTuple<T1, T2>
{
    public readonly T1 Item1;
    public readonly T2 Item2;
    public readonly Entity Entity;

    public QueryTuple(T1 item1, T2 item2, Entity entity)
    {
        Item1 = item1;
        Item2 = item2;
        Entity = entity;
    }

    public void Deconstruct(out T1 item1, out T2 item2)
    {
        item1 = Item1;
        item2 = Item2;
    }

    public void Deconstruct(out T1 item1, out T2 item2, out Entity entity)
    {
        item1 = Item1;
        item2 = Item2;
        entity = Entity;
    }
}
#endregion

#region T3
public readonly ref struct QueryTuple<T1, T2, T3>
{
    public readonly T1 Item1;
    public readonly T2 Item2;
    public readonly T3 Item3;
    public readonly Entity Entity;

    public QueryTuple(T1 item1, T2 item2, T3 item3, Entity entity)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
        Entity = entity;
    }

    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
    }

    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out Entity entity)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        entity = Entity;
    }
}
#endregion

#region T4
public readonly ref struct QueryTuple<T1, T2, T3, T4>
{
    public readonly T1 Item1;
    public readonly T2 Item2;
    public readonly T3 Item3;
    public readonly T4 Item4;
    public readonly Entity Entity;

    public QueryTuple(T1 item1, T2 item2, T3 item3, T4 item4, Entity entity)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
        Item4 = item4;
        Entity = entity;
    }


    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
    }

    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out Entity entity)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        entity = Entity;
    }
}
#endregion

#region T5
public readonly ref struct QueryTuple<T1, T2, T3, T4, T5>
{
    public readonly T1 Item1;
    public readonly T2 Item2;
    public readonly T3 Item3;
    public readonly T4 Item4;
    public readonly T5 Item5;
    public readonly Entity Entity;

    public QueryTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, Entity entity)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
        Item4 = item4;
        Item5 = item5;
        Entity = entity;
    }


    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
    }

    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out Entity entity)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
        entity = Entity;
    }
}
#endregion
