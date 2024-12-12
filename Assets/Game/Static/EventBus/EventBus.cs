using System;
using System.Collections.Generic;
using System.Linq;

public static class EventBus
{
    private static Dictionary<Type, List<Listener>> events = new Dictionary<Type, List<Listener>>();

    private class Listener //структуры хранятся в стеке, что думаю тут не подходит
    {
        public Action action;
        public int priority;

        public Listener(Action newAction, int newPriority)
        {
            action = newAction;
            priority = newPriority;
        }
    }


    public static void Add<T>(Action action, int priority = 0) where T : EventArgs
    {
        Type eventType = typeof(T);
        if (!events.ContainsKey(eventType))
        {
            events[eventType] = new List<Listener>();
        }

        events[eventType].Add(new Listener(action, priority));
        events[eventType].Sort((a, b) => b.priority.CompareTo(a.priority));
    }

    public static void Remove<T>(Action action) where T : EventArgs
    {
        Type eventType = typeof(T);
        if (events.ContainsKey(eventType))
        {
            var listener = events[eventType].FirstOrDefault(l => l.action == action);
            if (listener != null)
            {
                events[eventType].Remove(listener);
            }
        }
    }

    public static void Invoke<T>() where T : EventArgs
    {
        Type eventType = typeof(T);
        if (events.ContainsKey(eventType))
        {
            foreach (var listener in events[eventType])
            {
                listener.action.Invoke();
            }
        }
    }
}
