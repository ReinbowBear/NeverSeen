
public readonly ref struct QueryTuple<T1> where T1 : struct, IComponentData
{
    public readonly ComponentProxy<T1> Item1;

    public QueryTuple(ComponentProxy<T1> Item1)
    {
        this.Item1 = Item1;
    }


    public void Deconstruct(out ComponentProxy<T1> Item1)
    {
        Item1 = this.Item1;
    }
}


public readonly ref struct QueryTuple<T1, T2> where T1 : struct, IComponentData where T2 : struct, IComponentData
{
    public readonly ComponentProxy<T1> Item1;
    public readonly ComponentProxy<T2> Item2;

    public QueryTuple(ComponentProxy<T1> Item1, ComponentProxy<T2> Item2)
    {
        this.Item1 = Item1;
        this.Item2 = Item2;
    }


    public void Deconstruct(out ComponentProxy<T1> Item1, out ComponentProxy<T2> Item2)
    {
        Item1 = this.Item1;
        Item2 = this.Item2;
    }
}


public readonly ref struct QueryTuple<T1, T2, T3> where T1 : struct, IComponentData where T2 : struct, IComponentData where T3 : struct, IComponentData
{
    public readonly ComponentProxy<T1> Item1;
    public readonly ComponentProxy<T2> Item2;
    public readonly ComponentProxy<T3> Item3;

    public QueryTuple(ComponentProxy<T1> Item1, ComponentProxy<T2> Item2, ComponentProxy<T3> Item3)
    {
        this.Item1 = Item1;
        this.Item2 = Item2;
        this.Item3 = Item3;
    }


    public void Deconstruct(out ComponentProxy<T1> Item1, out ComponentProxy<T2> Item2, out ComponentProxy<T3> Item3)
    {
        Item1 = this.Item1;
        Item2 = this.Item2;
        Item3 = this.Item3;
    }
}


public readonly ref struct QueryTuple<T1, T2, T3, T4> where T1 : struct, IComponentData where T2 : struct, IComponentData where T3 : struct, IComponentData where T4 : struct, IComponentData
{
    public readonly ComponentProxy<T1> Item1;
    public readonly ComponentProxy<T2> Item2;
    public readonly ComponentProxy<T3> Item3;
    public readonly ComponentProxy<T4> Item4;

    public QueryTuple(ComponentProxy<T1> Item1, ComponentProxy<T2> Item2, ComponentProxy<T3> Item3, ComponentProxy<T4> Item4)
    {
        this.Item1 = Item1;
        this.Item2 = Item2;
        this.Item3 = Item3;
        this.Item4 = Item4;
    }


    public void Deconstruct(out ComponentProxy<T1> Item1, out ComponentProxy<T2> Item2, out ComponentProxy<T3> Item3, out ComponentProxy<T4> Item4)
    {
        Item1 = this.Item1;
        Item2 = this.Item2;
        Item3 = this.Item3;
        Item4 = this.Item4;
    }
}
