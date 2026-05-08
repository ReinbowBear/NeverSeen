using System;
using UnityEngine;

[Serializable]
public class TypeReference
{
    [SerializeField] private string typeName;
    [NonSerialized] private Type cached;

    public Type Type
    {
        get
        {
            if (cached == null && !string.IsNullOrEmpty(typeName))
                cached = Type.GetType(typeName);

            return cached;
        }
        set
        {
            typeName = value != null ? value.AssemblyQualifiedName : null;
            cached = value;
        }
    }
}
