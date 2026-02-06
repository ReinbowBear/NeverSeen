using System;
using System.Collections.Generic;
using UnityEngine;

public class ComponentBus
{
    private readonly Dictionary<EventKey, List<IComponentSubscriber>> eventMap = new();

    public void AddListener<T>(Action<T> callback, Enum eventType) where T : Component
    {
        var key = new EventKey(eventType);

        if (!eventMap.TryGetValue(key, out var list))
        {
            list = new List<IComponentSubscriber>();
            eventMap[key] = list;
        }

        list.Add(new ComponentSubscriber<T>(callback));
    }

    public void RemoveListener<T>(Action<T> callback, Enum eventType) where T : Component
    {
        var key = new EventKey(eventType);
        if (!eventMap.TryGetValue(key, out var list)) return;

        list.RemoveAll(s => s.Matches(callback));
        if (list.Count == 0) eventMap.Remove(key);
    }

    public void Invoke(Component component, Enum eventType)
    {
        var key = new EventKey(eventType);

        if (!eventMap.TryGetValue(key, out var list)) return;

        for (int i = 0; i < list.Count; i++)
        {
            list[i].TryInvoke(component);
        }
    }


    public List<IComponentSubscriber> GetSubscribers(Enum eventType)
    {
        var key = new EventKey(eventType);

        if (eventMap.TryGetValue(key, out var list))
        {
            return list;
        }
        return new();
    }
}


public readonly struct EventKey : IEquatable<EventKey>
{
    public readonly Type EnumType;
    public readonly int Value;

    public EventKey(Enum enumToKey)
    {
        EnumType = enumToKey.GetType();
        Value = Convert.ToInt32(enumToKey);
    }

    public bool Equals(EventKey other) => EnumType == other.EnumType && Value == other.Value;
    public override int GetHashCode() => HashCode.Combine(EnumType, Value);
}


public class ComponentSubscriber<T> : IComponentSubscriber where T : Component
{
    public Type ComponentType => typeof(T);
    private readonly Action<T> callback;

    public ComponentSubscriber(Action<T> callback)
    {
        this.callback = callback;
    }

    public void TryInvoke(Component component)
    {
        if (component is T typed)
        {
            callback(typed);
        }
    }

    public bool Matches(Delegate cb)
    {
        return callback.Equals(cb);
    }
}


public interface IComponentSubscriber
{
    Type ComponentType { get; }
    void TryInvoke(Component component);
    bool Matches(Delegate callback);
}
