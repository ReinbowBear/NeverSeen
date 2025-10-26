using System;
using System.Collections.Generic;

public class ComponentRegistry
{
    private readonly TypeRegistry typeRegistry;
    private readonly Dictionary<Type, IComponentStorage> storages = new();

    public ComponentRegistry(TypeRegistry typeRegistry)
    {
        this.typeRegistry = typeRegistry;
    }


    public void AddComponent<T>(ref Entity entity, T component) where T : struct, IComponentData
    {
        int bit = typeRegistry.GetIndex<T>();
        entity.AddComponentBit(bit);
        GetStorage<T>().Add(entity, component);
    }

    public void RemoveComponent<T>(ref Entity entity) where T : struct, IComponentData
    {
        int bit = typeRegistry.GetIndex<T>();
        entity.RemoveComponentBit(bit);
        GetStorage<T>().Remove(entity);
    }

    public void RemoveAllComponents(ref Entity entity)
    {
        foreach (var storage in storages.Values)
        {
            storage.Remove(entity);
        }
    }



    public bool HasComponent<T>(Entity entity) where T : struct, IComponentData
    {
        return GetStorage<T>().Has(entity);
    }

    public ref T GetComponent<T>(Entity entity) where T : struct, IComponentData
    {
        return ref GetStorage<T>().Get(entity);
    }


    private ComponentStorage<T> GetStorage<T>() where T : struct, IComponentData
    {
        var type = typeof(T);
        if (!storages.TryGetValue(type, out var storage))
        {
            storage = new ComponentStorage<T>();
            storages[type] = storage;
        }
        return (ComponentStorage<T>)storage;
    }
}
