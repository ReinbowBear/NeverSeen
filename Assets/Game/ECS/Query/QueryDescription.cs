
public sealed class QueryDescription
{
    public BitMask64 RequiredMask = new();
    public BitMask64 ExcludedMask = new();
    public BitMask64 ChangedMask = new();


    public QueryDescription Require<T>() where T : struct, IComponentData
    {
        RequiredMask.Add(ComponentType<T>.Index);
        return this;
    }

    public QueryDescription Exclude<T>() where T : struct, IComponentData
    {
        ExcludedMask.Add(ComponentType<T>.Index);
        return this;
    }

    public QueryDescription Changed<T>() where T : struct, IComponentData
    {
        int id = ComponentType<T>.Index;
        RequiredMask.Add(id);
        ChangedMask.Add(id);
        return this;
    }
}
