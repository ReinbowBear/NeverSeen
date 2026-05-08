using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class TypePickerAttribute : PropertyAttribute
{
    public Type BaseType;

    public TypePickerAttribute(Type baseType)
    {
        BaseType = baseType;
    }
}