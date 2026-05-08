using System;
using UnityEngine;

public class ComponentStore<T> : IComponentStore
{
    private Entity[] entities;
    private int[] sparse; // entity -> index
    private T[] components;

    private int count;
    public int Count => count;

    public Entity[] Entities => entities;
    public T[] Components => components;
    public int[] Sparse => sparse;

    public ComponentStore(int initialCapacity = 16)
    {
        entities = new Entity[initialCapacity];
        sparse = new int[initialCapacity];
        components = new T[initialCapacity];

        Array.Fill(sparse, -1);
    }


    public void AddWithCast(Entity entity, object component)
    {
        Add(entity, (T)component);
    }

    public void Add(Entity entity, T component)
    {
        int id = entity.Id;

        EnsureSparse(id);

        if (sparse[id] != -1)
        {
            components[sparse[id]] = component;
            return;
        }

        EnsureCapacity();

        int idx = count;

        entities[idx] = entity;
        components[idx] = component;

        sparse[id] = idx;

        count++;
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


    private void EnsureSparse(int id)
    {
        if (id < sparse.Length) return;

        int oldLength = sparse.Length;
        int newSize = Math.Max(sparse.Length * 2, id + 1);

        Array.Resize(ref sparse, newSize);
        Array.Fill(sparse, -1, oldLength, newSize - oldLength);
    }
    
    private void EnsureCapacity()
    {
        if (count < entities.Length) return;
    
        int newSize = entities.Length * 2;
    
        Array.Resize(ref entities, newSize);
        Array.Resize(ref components, newSize);
        Debug.Log($"Store <{typeof(T)}> new size: {entities.Length}");
    }
}
