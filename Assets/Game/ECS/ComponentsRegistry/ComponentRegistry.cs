using System;
using System.Collections.Generic;

public class ComponentRegistry
{
    private Dictionary<Type, IComponentStore> stores = new();
    private TypeRegistry typeRegistry;

    public ComponentRegistry(TypeRegistry typeRegistry)
    {
        this.typeRegistry = typeRegistry;
    }


    public bool HasEntityWith(Type type)
    {
        var chunk = GetStore(type);

        if (chunk.Count > 0) return true;
        else return false;
    }


    public Chunk<T> GetStore<T>()
    {
        Type type = typeof(T);
        if (!stores.TryGetValue(type, out var store))
        {
            int compIndex = typeRegistry.GetIndex<T>();
            var newStore = new Chunk<T>(compIndex);
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
            var compIndex = typeRegistry.GetIndex(type);

            store = (IComponentStore)Activator.CreateInstance(chunkType, compIndex, 16);
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
    int Count { get; }
    void AddWithCast(Entity entity, object component);
    bool Contains(Entity entity);
    void Remove(Entity entity);
}




public class StoreRegistry
{
    private Dictionary<int, IComponentStore> stores = new();


    public bool HasStore(int typeIndex)
    {
        return stores.ContainsKey(typeIndex);
    }


    public Chunk<T> CreateStore<T>(int typeIndex)
    {
        var newStore = new Chunk<T>(typeIndex);
        stores[typeIndex] = newStore;

        return newStore;
    }

    public IComponentStore GetStore(int typeIndex)
    {
        return stores[typeIndex];
    }


    public void Clear()
    {
        stores.Clear();
    }
}
