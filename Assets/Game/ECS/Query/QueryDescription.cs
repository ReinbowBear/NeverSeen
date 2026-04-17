using System;

public struct QueryDescription : IEquatable<QueryDescription>
{
    public BitMask64 RequiredMask;
    public BitMask64 ExcludedMask;

    private TypeRegistry typeRegistry;

    public QueryDescription(TypeRegistry typeRegistry)
    {
        RequiredMask = new();
        ExcludedMask = new();

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


    public bool Equals(QueryDescription other)
    {
        return RequiredMask.Equals(other.RequiredMask) 
            && ExcludedMask.Equals(other.ExcludedMask);
    }

    public override bool Equals(object obj) => obj is QueryDescription other && Equals(other);

    public override int GetHashCode()
    {
        return HashCode.Combine(RequiredMask, ExcludedMask);
    }
}
