using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// компоненты хранятся в массивах потому что лист\словарь почему то возращают временную !копию! структуры и её нельзя вернуть как ref
// массив оптимизирован, он создаёт много места и удваивает его если оно кончилось. стандартный ЕКС подход как оказалось
public sealed class ComponentStorage<T> : IComponentStorage where T : struct, IComponentData
{
    private Entity[] entities;
    private T[] components;
    private readonly Dictionary<int, int> lookup = new(); // EntityId → componentsId
    private int count;

    public ComponentStorage(int initialCapacity = 64)
    {
        entities = new Entity[initialCapacity];
        components = new T[initialCapacity];
    }

    public ReadOnlySpan<Entity> Entities => entities.AsSpan(0, count);
    public ReadOnlySpan<T> Components => components.AsSpan(0, count);

    public void Add(Entity entity, T component)
    {
        if (lookup.TryGetValue(entity.Id, out int index))
        {
            components[index] = component;
            return;
        }

        if (count == components.Length)
        {
            Resize();
        }

        entities[count] = entity;
        components[count] = component;
        lookup[entity.Id] = count;
        count++;
    }

    public bool Remove(Entity entity)
    {
        if (!lookup.TryGetValue(entity.Id, out int index)) return false;

        int last = count - 1;
        if (index != last)
        {
            components[index] = components[last];
            entities[index] = entities[last];
            lookup[entities[index].Id] = index;
        }

        lookup.Remove(entity.Id);
        count--;
        return true;
    }


    public bool Has(Entity entity)
    {
        return lookup.ContainsKey(entity.Id);
    }

    public ref T Get(Entity entity)
    {
        return ref components[lookup[entity.Id]];
    }


    private void Resize()
    {
        int newSize = components.Length * 2;
        Array.Resize(ref components, newSize);
        Array.Resize(ref entities, newSize);
    }
}

public interface IComponentStorage // интерфейс необходим для полиморфного списка, так как основной класс дженерик
{
    bool Remove(Entity entity);
}

//public interface IComponentStorage<T> where T : struct, IComponentData
//{
//    bool Has(Entity entity);
//    ref T Get(Entity entity);
//    void Add(Entity entity, T component);
//    bool Remove(Entity entity);
//
//}
