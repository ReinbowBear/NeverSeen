
#region T2
public readonly ref struct QueryTuple<T1, T2>
{
    public readonly T1 Item1;
    public readonly T2 Item2;

    public QueryTuple(T1 item1, T2 item2)
    {
        Item1 = item1;
        Item2 = item2;
    }

    public void Deconstruct(out T1 item1, out T2 item2)
    {
        item1 = Item1;
        item2 = Item2;
    }
}
#endregion

#region T3
public readonly ref struct QueryTuple<T1, T2, T3>
{
    public readonly T1 Item1;
    public readonly T2 Item2;
    public readonly T3 Item3;

    public QueryTuple(T1 item1, T2 item2, T3 item3)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
    }

    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
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

    public QueryTuple(T1 item1, T2 item2, T3 item3, T4 item4)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
        Item4 = item4;
    }


    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
    }
}
#endregion

#region T4
public readonly ref struct QueryTuple<T1, T2, T3, T4, T5>
{
    public readonly T1 Item1;
    public readonly T2 Item2;
    public readonly T3 Item3;
    public readonly T4 Item4;
    public readonly T5 Item5;

    public QueryTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
        Item4 = item4;
        Item5 = item5;
    }


    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5)
    {
        item1 = Item1;
        item2 = Item2;
        item3 = Item3;
        item4 = Item4;
        item5 = Item5;
    }
}
#endregion
