using System;
using System.Collections.Generic;

public class Chunk<T> : IComponentStore
{
    private SparseSet<Entity> entities;
    private T[] components;
    private int[] versions;

    public int Count => entities.Count;

    public Chunk(int initialCapacity = 16)
    {
        entities = new SparseSet<Entity>(initialCapacity);
        components = new T[Math.Max(initialCapacity, 16)];
        versions = new int[Math.Max(initialCapacity, 16)];
    }


    public void Add(Entity entity, object component)
    {
        Add(entity, (T)component);
    }

    public void Add(Entity entity, T component)
    {
        if (entities.Contains(entity))
        {
            int idx = entities.IndexOf(entity);
            components[idx] = component;
            versions[idx]++;
            return;
        }

        if (entities.Count >= components.Length)
        {
            Array.Resize(ref components, components.Length * 2);
            Array.Resize(ref versions, versions.Length * 2);
        }

        entities.Add(entity);

        int idxNew = entities.Count - 1;
        components[idxNew] = component;
        versions[idxNew] = 1;
    }

    public void Remove(Entity entity)
    {
        int idx = entities.IndexOf(entity);
        if (idx == -1) return;

        int last = entities.Count - 1;
        entities.Remove(entity);

        if (idx != last)
        {
            components[idx] = components[last];
            versions[idx] = versions[last];
        }
    }

    public T GetRO(Entity entity)
    {
        int idx = entities.IndexOf(entity);
        if (idx == -1) throw new KeyNotFoundException($"Entity {entity.Id} not found");

        return components[idx];
    }

    public T GetRW(Entity entity)
    {
        int idx = entities.IndexOf(entity);
        if (idx == -1) throw new KeyNotFoundException($"Entity {entity.Id} not found");

        versions[idx]++;
        return components[idx];
    }

    public int GetVersion(Entity entity)
    {
        int idx = entities.IndexOf(entity);
        if (idx == -1) throw new KeyNotFoundException($"Entity {entity.Id} not found");

        return versions[idx];
    }
}
