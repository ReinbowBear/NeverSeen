
public readonly ref struct ComponentProxy<T>
{
    private readonly Chunk<T> chunk;
    private readonly Entity entity;

    public ComponentProxy(Chunk<T> chunk, ref Entity entity)
    {
        this.chunk = chunk;
        this.entity = entity;
    }


    public Entity Entity => entity;
    public T Read => chunk.GetRO(Entity);
    public T Write => chunk.GetRW(Entity);
}
