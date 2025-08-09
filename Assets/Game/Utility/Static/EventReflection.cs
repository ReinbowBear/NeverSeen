using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

public static class EventReflection
{
    private static readonly Dictionary<Type, Action<(Delegate, int)>> addDelegates = new();
    private static readonly Dictionary<Type, Action<(Delegate, int)>> removeDelegates = new();

    public static void SubscribeClass(object target, bool isSubscribe)
    {
        var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
        .Where(m => m.GetCustomAttribute<EventHandlerAttribute>() != null);

        foreach (var method in methods)
        {
            var parameters = method.GetParameters(); // смотрим аргументы функции
            if (parameters.Length != 1) continue;

            var eventType = parameters[0].ParameterType;
            if (!typeof(EventArgs).IsAssignableFrom(eventType)) continue;

            var delegateType = typeof(Action<>).MakeGenericType(eventType);

            Delegate handlerDelegate;
            try 
            {
                handlerDelegate = Delegate.CreateDelegate(delegateType, target, method);
            }
            catch
            {
                continue;
            }

            var attribute = method.GetCustomAttribute<EventHandlerAttribute>();
            int priority = attribute?.Priority ?? 0;

            var delegateInvoker = GetInvoker(eventType, isSubscribe); // функция кеширует делегат, оптимизация
            delegateInvoker?.Invoke((handlerDelegate, priority));
        }
    }

    private static Action<(Delegate, int)> GetInvoker(Type eventType, bool isSubscribe)
    {
        var dictionary = isSubscribe ? addDelegates : removeDelegates;
        if (dictionary.TryGetValue(eventType, out var eventDelegate)) return eventDelegate;

        var eventBusMethod = typeof(EventBus).GetMethod(isSubscribe ? "Add" : "Remove").MakeGenericMethod(eventType);

        var tupleParam = Expression.Parameter(typeof((Delegate, int)), "arg");
        var handlerPart = Expression.Convert(Expression.Field(tupleParam, "Item1"), typeof(Action<>).MakeGenericType(eventType));
        var priorityPart = Expression.Field(tupleParam, "Item2");

        MethodCallExpression call;
        if (isSubscribe)
        {
            call = Expression.Call(eventBusMethod, handlerPart, priorityPart);
        }
        else
        {
            call = Expression.Call(eventBusMethod, handlerPart);
        }

        var lambda = Expression.Lambda<Action<(Delegate, int)>>(call, tupleParam);
        var compiled = lambda.Compile();

        dictionary[eventType] = compiled;
        return compiled;
    }
}


[AttributeUsage(AttributeTargets.Method)]
public class EventHandlerAttribute : Attribute
{
    public int Priority { get; set; }

    public EventHandlerAttribute(int priority = 0)
    {
        Priority = priority;
    }
}
