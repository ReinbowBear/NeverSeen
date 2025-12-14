
public sealed class QueryBuilder
{
    private readonly ComponentTypeRegistry typeRegistry;
    private readonly QueryDescription desc = new();

    public QueryBuilder(ComponentTypeRegistry typeRegistry)
    {
        this.typeRegistry = typeRegistry;
    }


    public QueryBuilder WithRead<T>() where T : struct, IComponentData
    {
        int index = typeRegistry.GetIndex(typeof(T));
        desc.RequiredMask.Add(index);
        return this;
    }

    public QueryBuilder WithWrite<T>() where T : struct, IComponentData
    {
        int index = typeRegistry.GetIndex(typeof(T));
        desc.ChangedMask.Add(index);
        return this;
    }

    public QueryBuilder WithNone<T>() where T : struct, IComponentData
    {
        int index = typeRegistry.GetIndex(typeof(T));
        desc.ExcludedMask.Add(index);
        return this;
    }
}
