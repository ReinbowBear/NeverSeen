using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

public static class EventReflection
{
    private static readonly Dictionary<Type, Action<(Delegate, Priority)>> addDelegates = new();
    private static readonly Dictionary<Type, Action<(Delegate, Priority)>> removeDelegates = new(); // Priority ЗДЕСЬ НЕ НУЖЕН, но так код проще и короче

    private static readonly MethodInfo AddMethod;
    private static readonly MethodInfo RemoveMethod;

    static EventReflection() // кэш
    {
        AddMethod = typeof(EventBus).GetMethod("Subscribe");
        RemoveMethod = typeof(EventBus).GetMethod("Unsubscribe");
    }


    public static void SubscribeClass(object target, bool isSubscribe)
    {
        var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
        .Where(m => m.GetCustomAttribute<EventHandlerAttribute>() != null);

        foreach (var method in methods)
        {
            var parameters = method.GetParameters();
            if (parameters.Length != 1)
            {
                Debug.LogWarning($"Метод {method.Name} должен иметь 1 параметр (события)");
                continue;
            }

            var eventType = parameters[0].ParameterType;
            var delegateType = typeof(Action<>).MakeGenericType(eventType);

            Delegate handlerDelegate;
            try
            {
                handlerDelegate = Delegate.CreateDelegate(delegateType, target, method);
            }
            catch
            {
                Debug.LogWarning($"ошибка создания делегата для! {method.Name}");
                continue;
            }

            var attribute = method.GetCustomAttribute<EventHandlerAttribute>();
            Priority priority = attribute.Priority;

            var delegateInvoker = Get(eventType, isSubscribe);
            delegateInvoker?.Invoke((handlerDelegate, priority));
        }
    }

    private static Action<(Delegate, Priority)> Get(Type eventType, bool isSubscribe) // функция проверяет зарегестрировавны ли уже в кэше ивенты
    {
        var cache = isSubscribe ? EventBusRegister.addDelegates : EventBusRegister.removeDelegates;

        if (cache.TryGetValue(eventType, out var invoker)) return invoker;
        else return GetInvoker(eventType, isSubscribe);
    }


    private static Action<(Delegate, Priority)> GetInvoker(Type eventType, bool isSubscribe) // ран тайм генерация события
    {
        var dictionary = isSubscribe ? addDelegates : removeDelegates;
        if (dictionary.TryGetValue(eventType, out var eventDelegate)) return eventDelegate;

        MethodInfo genericMethod = (isSubscribe ? AddMethod : RemoveMethod).MakeGenericMethod(eventType);

        var lambdaParam = Expression.Parameter(typeof((Delegate, Priority)), "arg"); // arg просто метка
        var handlerPart = Expression.Convert(Expression.Field(lambdaParam, "Item1"), typeof(Action<>).MakeGenericType(eventType)); // lambdaParam, "Item1" айтем это Delegate, первый параметр

        MethodCallExpression call;
        if (isSubscribe)
        {
            var priorityPart = Expression.Field(lambdaParam, "Item2");
            call = Expression.Call(genericMethod, handlerPart, priorityPart);
        }
        else
        {
            call = Expression.Call(genericMethod, handlerPart);
        }

        var lambda = Expression.Lambda<Action<(Delegate, Priority)>>(call, lambdaParam);
        var compiled = lambda.Compile();

        dictionary[eventType] = compiled;
        return compiled;
    }
}


[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class EventHandlerAttribute : Attribute
{
    public Priority Priority { get; }

    public EventHandlerAttribute(Priority priority = 0)
    {
        Priority = priority;
    }
}
