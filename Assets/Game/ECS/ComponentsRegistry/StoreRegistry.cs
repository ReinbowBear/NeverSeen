using System;

public class StoreRegistry
{
    private IComponentStore[] stores = new IComponentStore[64];

    public ComponentStore<T> GetOrCreateStore<T>(int typeIndex)
    {
        if (typeIndex > stores.Length)
        {
            EnsureCapacity(typeIndex);
        }

        if (stores[typeIndex] == null)
        {
            stores[typeIndex] = new ComponentStore<T>();
        }

        return (ComponentStore<T>)stores[typeIndex];
    }

    public IComponentStore GetOrCreateStore(int typeIndex, Type type)
    {
        if (typeIndex > stores.Length)
        {
            EnsureCapacity(typeIndex);
        }

        if (stores[typeIndex] == null)
        {
            var generic = typeof(ComponentStore<>).MakeGenericType(type);
            stores[typeIndex] = (IComponentStore)Activator.CreateInstance(generic, 16);
        }

        return stores[typeIndex];
    }


    public IComponentStore GetStore(int typeIndex)
    {
        return stores[typeIndex];
    }


    private void EnsureCapacity(int index)
    {
        if (index < stores.Length) return;

        int newSize = Math.Max(stores.Length * 2, index + 1);
        Array.Resize(ref stores, newSize);
    }


    public void Clear()
    {
        Array.Clear(stores, 0, stores.Length);
    }
}
