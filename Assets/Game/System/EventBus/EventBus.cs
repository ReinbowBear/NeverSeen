using System;
using System.Collections.Generic;

public class EventBus
{
    private readonly Dictionary<CombineKey, SortedDictionary<Priority, HashSet<Action>>> eventMap = new();

    public void AddListener<T>(Action callback, Priority priority, object scope = null)
    {
        var key = new CombineKey(typeof(T), scope);

        if (!eventMap.TryGetValue(key, out var priorityMap))
        {
            priorityMap = new SortedDictionary<Priority, HashSet<Action>>();
            eventMap[key] = priorityMap;
        }

        if (!priorityMap.TryGetValue(priority, out var subscribers))
        {
            subscribers = new HashSet<Action>();
            priorityMap[priority] = subscribers;
        }

        subscribers.Add(callback);
    }

    public void RemoveListener<T>(Action callback, object scope = null)
    {
        var key = new CombineKey(typeof(T), scope);

        if (eventMap.TryGetValue(key, out var priorityMap))
        {
            foreach (var subscribers in priorityMap.Values)
            {
                subscribers.Remove(callback);
            }
        }
    }


    public void Invoke<T>(object scope = null) where T : EventArgs
    {
        var key = new CombineKey(typeof(T), scope);

        if (eventMap.TryGetValue(key, out var priorityMap))
        {
            foreach (var priorityHash in priorityMap)
            {
                foreach (var callback in priorityHash.Value)
                {
                    callback.Invoke();
                }
            }
        }
    }
}


public enum Priority
{
    Init, Logic, View,
}

public readonly struct CombineKey : IEquatable<CombineKey>
{
    public readonly Type EventType;
    public readonly object SubKey;

    public CombineKey(Type type, object subKey)
    {
        EventType = type;
        SubKey = subKey;
    }

    private bool Equals(CombineKey other) => EventType == other.EventType && Equals(SubKey, other.SubKey);
    public override bool Equals(object obj) => obj is CombineKey other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(EventType, SubKey);

    bool IEquatable<CombineKey>.Equals(CombineKey other)
    {
        return Equals(other);
    }
}
