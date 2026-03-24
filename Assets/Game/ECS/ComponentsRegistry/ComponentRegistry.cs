using System;
using System.Collections.Generic;

public class ComponentRegistry
{
    private Dictionary<Type, IComponentStore> stores = new();

    public Chunk<T> GetStore<T>()
    {
        Type type = typeof(T);
        if (!stores.TryGetValue(type, out var store))
        {
            var newStore = new Chunk<T>();
            stores[type] = newStore;
            return newStore;
        }

        return (Chunk<T>)store;
    }

    public IComponentStore GetStore(Type type)
    {
        if (!stores.TryGetValue(type, out var store))
        {
            var chunkType = typeof(Chunk<>).MakeGenericType(type);
            store = (IComponentStore)Activator.CreateInstance(chunkType)!;
            stores[type] = store;
        }

        return store;
    }


    public void Clear()
    {
        stores.Clear();
    }
}

public interface IComponentStore
{
    void Add(Entity entity, object component);
    void Remove(Entity entity);
}
