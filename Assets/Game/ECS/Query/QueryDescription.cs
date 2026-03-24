
public struct QueryDescription
{
    public BitMask64 RequiredMask;
    public BitMask64 ExcludedMask;
    public BitMask64 ChangedMask;

    private TypeRegistry typeRegistry;

    public QueryDescription(TypeRegistry typeRegistry)
    {
        RequiredMask = new();
        ExcludedMask = new();
        ChangedMask = new();

        this.typeRegistry = typeRegistry;
    }


    public QueryDescription Require<T>()
    {
        var index = typeRegistry.GetIndex(typeof(T));
        RequiredMask.Add(index);
        return this;
    }

    public QueryDescription Exclude<T>()
    {
        var index = typeRegistry.GetIndex(typeof(T));
        ExcludedMask.Add(index);
        return this;
    }

    public QueryDescription Changed<T>()
    {
        var index = typeRegistry.GetIndex(typeof(T));
        RequiredMask.Add(index);
        ChangedMask.Add(index);
        return this;
    }
}
