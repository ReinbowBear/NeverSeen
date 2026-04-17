using System;

public class Chunk<T> : IComponentStore
{
    private Entity[] entities;
    private T[] components;
    private int[] sparse; // entity -> index

    private int count;
    public int Count => count;

    private readonly int CompIndex;

    public Chunk(int compIndex, int initialCapacity = 16)
    {
        CompIndex = compIndex;

        entities = new Entity[initialCapacity];
        components = new T[initialCapacity];
        sparse = new int[initialCapacity];

        Array.Fill(sparse, -1);
    }


    public void AddWithCast(Entity entity, object component)
    {
        Add(entity, (T)component);
    }

    public void Add(Entity entity, T component)
    {
        int id = entity.Id;

        CheckSparseCapacity(id);

        if (sparse[id] != -1)
        {
            components[sparse[id]] = component;
            return;
        }

        TryResizeCapacity();

        int idx = count;

        entities[idx] = entity;
        components[idx] = component;

        sparse[id] = idx;

        count++;

        entity.Mask.Add(CompIndex);
    }

    public void Remove(Entity entity)
    {
        int id = entity.Id;
        int idx = sparse[id];

        if (idx == -1) return;

        int last = count - 1;

        entities[idx] = entities[last];
        components[idx] = components[last];

        sparse[entities[idx].Id] = idx;
        sparse[id] = -1;
        count--;

        entity.Mask.Remove(CompIndex);
    }


    public bool Contains(Entity entity)
    {
        int id = entity.Id;
        return id < sparse.Length && sparse[id] != -1;
    }

    public T GetComponent(Entity entity)
    {
        return components[sparse[entity.Id]];
    }


    private void CheckSparseCapacity(int id)
    {
        if (id < sparse.Length) return;
    
        int newSize = sparse.Length;

        while (newSize <= id)
        {
            newSize *= 2;
        }
    
        Array.Resize(ref sparse, newSize);
        Array.Fill(sparse, -1, sparse.Length, newSize - sparse.Length);
    }
    
    private void TryResizeCapacity()
    {
        if (count < entities.Length) return;
    
        int newSize = entities.Length * 2;
    
        Array.Resize(ref entities, newSize);
        Array.Resize(ref components, newSize);
    }
}
