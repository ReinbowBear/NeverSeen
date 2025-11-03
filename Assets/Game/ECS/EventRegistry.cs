using System;
using System.Collections.Generic;

public sealed class EventRegistry
{
    private readonly List<Type> activeEvents = new();
    private readonly Dictionary<Type, List<ISystem>> eventSystems = new();

    public void Invoke<T>()
    {
        activeEvents.Add(typeof(T));
    }

    public bool IsInvoke<T>()
    {
        return activeEvents.Contains(typeof(T));
    }

    public void Clear()
    {
        activeEvents.Clear();
    }
}