
public readonly ref struct ComponentProxy<T> where T : struct, IComponentData // класс должен быть дженерик так или иначе что бы возращать Т
{
    private readonly Chunk chunk;
    private readonly int slot;
    private readonly int compIndex;

    public ComponentProxy(Chunk chunk, int slot, int compIndex)
    {
        this.chunk = chunk;
        this.slot = slot;
        this.compIndex = compIndex;
    }


    public ref readonly T Read => ref ((T[])chunk.ComponentArrays[compIndex])[slot]; // эти свойства можно переписать на методы чанка но тогда оптимизация малость хуже
    public ref T Write
    {
        get
        {
            chunk.ChangeMasks[compIndex].Add(slot);
            return ref ((T[])chunk.ComponentArrays[compIndex])[slot];
        }
    }
}
