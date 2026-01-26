using System;
using System.Collections.Generic;

public class EventBus
{
    private Dictionary<Enum, HashSet<Action>> eventMap = new();

    public void AddListener(Action callback, Enum eventType)
    {
        if (!eventMap.TryGetValue(eventType, out var subscribers))
        {
            subscribers = new HashSet<Action>();
            eventMap[eventType] = subscribers;
        }

        subscribers.Add(callback);
    }

    public void RemoveListener(Action callback, Enum eventType)
    {
        if (!eventMap.TryGetValue(eventType, out var subscribers)) return;

        subscribers.Remove(callback);

        if (subscribers.Count == 0) eventMap.Remove(eventType);
    }


    public void Invoke(Enum eventType)
    {
        if (!eventMap.TryGetValue(eventType, out var subscribers)) return;

        foreach (var action in subscribers)
        {
            action.Invoke();
        }
    }
}

