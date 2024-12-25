using System;
using System.Collections.Generic;
using System.Linq;

public static class EventBus
{
    private static Dictionary<Type, List<Listener>> events = new Dictionary<Type, List<Listener>>();

    private class Listener //структуры хранятся в стеке, что думаю тут не подходит
    {
        public Delegate action;
        public int priority;

        public Listener(Delegate newAction, int newPriority)
        {
            action = newAction;
            priority = newPriority;
        }
    }


    public static void Add<T>(Action<T> action, int priority = 0) where T : EventArgs
    {
        Type eventType = typeof(T);
        if (!events.ContainsKey(eventType))
        {
            events[eventType] = new List<Listener>();
        }

        events[eventType].Add(new Listener(action, priority));
        events[eventType].Sort((a, b) => b.priority.CompareTo(a.priority));
    }

    public static void Remove<T>(Action<T> action) where T : EventArgs
    {
        Type eventType = typeof(T);
        if (events.ContainsKey(eventType))
        {
            var listener = events[eventType].FirstOrDefault(l => Delegate.Equals(l.action, action)); //Delegate.Equals проверяет, совпадают ли делегаты по типу и содержимому, правильное сравнение
            if (listener != null)
            {
                events[eventType].Remove(listener);
            }
        }
    }

    public static void Invoke<T>(T eventArgs) where T : EventArgs
    {
        Type eventType = typeof(T);
        if (events.ContainsKey(eventType))
        {
            foreach (var listener in events[eventType])
            {
                if (listener.action is Action<T> typedAction)
                {
                    typedAction.Invoke(eventArgs); 
                }
            }
        }
    }
}
