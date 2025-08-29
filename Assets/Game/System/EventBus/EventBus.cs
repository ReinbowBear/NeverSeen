using System;
using System.Collections.Generic;
using System.Linq;

public static class EventBus
{
    private static Dictionary<Type, List<Listener>> subscribers = new();
    private static Dictionary<Type, bool> needSorting = new();

    static EventBus()
    {
        AddSubscriber<OnSceneRelease>(Clear);
    }

    private class Listener
    {
        public Delegate Callback;
        public Priority Priority;

        public Listener(Delegate newcallback, Priority priority)
        {
            Callback = newcallback;
            Priority = priority;
        }
    }


    public static void AddSubscriber<T>(Action<T> callback, Priority priority = 0)
    {
        Type eventType = typeof(T);

        if (!subscribers.ContainsKey(eventType))
        {
            subscribers[eventType] = new List<Listener>();
        }

        subscribers[eventType].Add(new Listener(callback, priority));
        needSorting[eventType] = true;
    }

    public static void RemoveSubscriber<T>(Action<T> callback)
    {
        Type eventType = typeof(T);
        if (!subscribers.TryGetValue(eventType, out var list)) return;

        var listener = list.FirstOrDefault(mylistener => Equals(mylistener.Callback, callback));

        if (listener != null)
        {
            list.Remove(listener);


            if (list.Count == 0)
            {
                subscribers.Remove(eventType);
            }
        }
    }


    public static void Invoke<T>(T eventData = default)
    {
        Type eventType = typeof(T);
        if (!subscribers.TryGetValue(eventType, out var list)) return;

        Sort(eventType);
        foreach (var listener in list)
        {
            if (listener.Callback is Action<T> EventType)
            {
                EventType.Invoke(eventData);
            }
        }
    }

    private static void Sort(Type type) // фани идея не сортировать списки вообще а создать на каждое событие списки с разными приоритетами
    {
        if (needSorting.TryGetValue(type, out bool isNeedSorting) && isNeedSorting)
        {
            subscribers[type].Sort((a, b) => b.Priority.CompareTo(a.Priority));
            needSorting[type] = false;
        }
    }


    private static void Clear(OnSceneRelease _)
    {
        subscribers = new();
        needSorting = new();

        AddSubscriber<OnSceneRelease>(Clear);
    }
}

public enum Priority
{
    none = 0,
    low = 10,
    normal = 20,
    high = 30,
    critical = 100
}
