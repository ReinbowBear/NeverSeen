using System;
using System.Collections.Generic;
using System.Linq;

public class EventBus
{
    private Dictionary<Enum, List<Action>> eventMap = new();

    public void AddListener(Action callback, Enum eventType)
    {
        if (!eventMap.TryGetValue(eventType, out var subscribers))
        {
            subscribers = new();
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


    public List<Action> GetSubscribers(Enum eventType)
    {
        if (eventMap.TryGetValue(eventType, out var subscribers))
        {
            return subscribers;
        }
        return new();
    }
}
