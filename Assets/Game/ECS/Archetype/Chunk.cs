using System;
using System.Collections.Generic;

public sealed class Chunk
{
    public const int Capacity = 256;
    public bool HasSpace => Entities.Count < Capacity;

    public readonly SparseSet<Entity> Entities;
    public readonly Array[] ComponentArrays;
    public readonly BitMaskSet[] ChangeMasks; // есть идея создать списки на каждый тип компонента, хранящие индекс изменённой сущности. системы просто on off делают если список не пустой

    public Chunk(List<Type> ComponentTypes)
    {
        int componentCount = ComponentTypes.Count;

        Entities = new SparseSet<Entity>(Capacity);
        ComponentArrays = new Array[componentCount];
        ChangeMasks = new BitMaskSet[componentCount];

        for (int i = 0; i < componentCount; i++)
        {
            Type type = ComponentTypes[i];
            ComponentArrays[i] = Array.CreateInstance(type, Capacity);
            ChangeMasks[i] = new BitMaskSet(Capacity);
        }
    }


    public int AddEntity(Entity entity, object[] components)
    {
        Entities.Add(entity);
        int entitySlot = Entities.Count - 1;

        for (int i = 0; i < ComponentArrays.Length; i++)
        {
            ComponentArrays[i].SetValue(components[i], entitySlot);
        }
        return entitySlot;
    }

    public void RemoveEntity(Entity entity) // перезаписываем компоненты в случаи добавления новой сущности
    {
        Entities.Remove(entity); // баг, нужно менять местами компоненты так же (маски не обязательно они очищаются)
    }


    public ref T GetComponentRO<T>(int slot, int typeIndex)
    {
        var array = (T[])ComponentArrays[typeIndex];
        return ref array[slot];
    }

    public ref T GetComponentRW<T>(int slot, int typeIndex)
    {
        ChangeMasks[typeIndex].Add(slot);

        var array = (T[])ComponentArrays[typeIndex];
        return ref array[slot];
    }


    public object[] GetEntityComponents(int indexInChunk)
    {
        object[] components = new object[ComponentArrays.Length];

        for (int i = 0; i < ComponentArrays.Length; i++)
        {
            components[i] = ComponentArrays[i].GetValue(indexInChunk);
        }

        return components;
    }


    public void ClearMasks()
    {
        foreach (var mask in ChangeMasks)
        {
            mask.Clear();
        }
    }
}
