using System;

public struct QueryMask : IEquatable<QueryMask>
{
    public BitMask64 RequiredMask;
    public BitMask64 ExcludedMask;


    public QueryMask Require(int index)
    {
        RequiredMask.Add(index);
        return this;
    }

    public QueryMask Exclude(int index)
    {
        ExcludedMask.Add(index);
        return this;
    }


    public bool Equals(QueryMask other)
    {
        return RequiredMask.Equals(other.RequiredMask) 
            && ExcludedMask.Equals(other.ExcludedMask);
    }

    public override bool Equals(object obj) => obj is QueryMask other && Equals(other);

    public override int GetHashCode()
    {
        return HashCode.Combine(RequiredMask, ExcludedMask);
    }
}
