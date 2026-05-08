
public static class TypeId
{
    public static int NextId = 0;
}

public static class TypeId<T>
{
    public static readonly int Id = ++TypeId.NextId;
}
