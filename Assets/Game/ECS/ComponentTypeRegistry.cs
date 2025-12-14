using System;
using System.Collections.Generic;

public class ComponentTypeRegistry
{
    public static ComponentTypeRegistry Instance { get; private set; }

    private readonly Dictionary<Type, int> typeToIndex = new();
    private int nextIndex = 0;

    public ComponentTypeRegistry()
    {
        Instance = this;
    }


    public int RegisterType(Type type) // вызывается статистическим конструктором класса ComponentType
    {
        if (typeToIndex.TryGetValue(type, out var existing)) return existing;

        int index = nextIndex++;
        typeToIndex[type] = index;

        return index;
    }


    public int GetIndex<T>() where T : struct, IComponentData
    {
        return ComponentType<T>.Index;
    }

    public int GetIndex(Type type)
    {
        if (typeToIndex.TryGetValue(type, out var existing)) return existing;
        return -1;
    }
}


public static class ComponentType<T> where T : struct, IComponentData // статический класс дает предельную оптимизацию, другой вариант кодогенерация енамов
{
    public static readonly int Index;
    
    static ComponentType()
    {
        Index = ComponentTypeRegistry.Instance.RegisterType(typeof(T));
    }
}
