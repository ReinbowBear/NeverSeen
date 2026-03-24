using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class TypeRegistry
{
    private Dictionary<Type, int> typeToIndex = new();
    private List<Type> indexToType = new();
    private List<int> indexToSize = new();

    public int GetIndex(Type type)
    {
        if (typeToIndex.TryGetValue(type, out var existing)) return existing;
    
        int index = indexToType.Count;
    
        typeToIndex[type] = index;
        indexToType.Add(type);
        indexToSize.Add(Marshal.SizeOf(type));
    
        return index;
    }

    public Type GetType(int typeIndex)
    {
        return indexToType[typeIndex];
    }


    public BitMask64 GetMask(object[] components)
    {
        var mask = new BitMask64();

        for (int i = 0; i < components.Length; i++)
        {
            var type = components[i].GetType();
            var index = GetIndex(type);
            mask.Add(index);
        }

        return mask;
    }
}
