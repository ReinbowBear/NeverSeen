using System;
using System.Collections.Generic;
using UnityEngine;

public class TypeRegistry
{
    private Dictionary<Type, int> typeToIndex = new();
    private int nextIndex = 0;

    public int GetIndex<T>()
    {
        var type = typeof(T);
        if (!typeToIndex.TryGetValue(type, out var index))
        {
            index = nextIndex++;
            if (index >= 64)
            {
                Debug.Log("сейчас максимум 64 компонента на всю игру!");
                return -1;
            }
            typeToIndex[type] = index;
        }
        return index;
    }
}
