using System;
using System.Collections.Generic;

public class TypeRegistry
{
    private Dictionary<Type, int> typeToIndex = new();
    private List<Type> indexToType = new();

    public int GetIndex<T>()
    {
        return GetIndex(typeof(T));
    }

    public int GetIndex(Type type)
    {
        if (typeToIndex.TryGetValue(type, out var existing)) return existing;
    
        int index = indexToType.Count;
    
        typeToIndex[type] = index;
        indexToType.Add(type);

        return index;
    }

    public Type GetType(int typeIndex)
    {
        return indexToType[typeIndex];
    }
}
