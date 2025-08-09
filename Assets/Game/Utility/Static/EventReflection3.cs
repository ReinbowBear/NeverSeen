
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class EventReflection3
{
    public static void EventReflection(object target, bool isSubscribe)
    {
        var methods = target.GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
            .Where(m => m.GetCustomAttribute<EventHandlerAttribute>() != null);
    
        foreach (var method in methods)
        {
            var parameters = method.GetParameters();
            if (parameters.Length != 1) continue;
    
            var eventType = parameters[0].ParameterType;
            if (!typeof(EventArgs).IsAssignableFrom(eventType)) continue;
    
            if (!EventBusInvoker.TryGetInvoker(eventType, isSubscribe, out var invoker))
                continue;
    
            try
            {
                var delegateType = typeof(Action<>).MakeGenericType(eventType);
                var handler = Delegate.CreateDelegate(delegateType, target, method);
                invoker(handler); // Вызывает EventBus.Add<T>((Action<T>)handler)
            }
            catch
            {
                // логгируй, если надо
                continue;
            }
        }
    }
}



public static class EventBusInvoker
{
    private static readonly Dictionary<Type, Action<object>> _addDelegates = new();
    private static readonly Dictionary<Type, Action<object>> _removeDelegates = new();

    static EventBusInvoker()
    {
        //Register<PlayerDiedEvent>();
        //Register<GameStartedEvent>();
        // Добавь остальные события здесь
    }

    private static void Register<T>() where T : EventArgs
    {
        _addDelegates[typeof(T)] = handler => EventBus.Add((Action<T>)handler);
        _removeDelegates[typeof(T)] = handler => EventBus.Remove((Action<T>)handler);
    }

    public static bool TryGetInvoker(Type eventType, bool isSubscribe, out Action<object> invoker)
    {
        return (isSubscribe ? _addDelegates : _removeDelegates).TryGetValue(eventType, out invoker);
    }
}
