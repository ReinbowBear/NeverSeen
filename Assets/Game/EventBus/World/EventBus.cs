using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{
    private Dictionary<Enum, List<ISubscribe>> eventMap = new();

    public void AddListener(ISubscribe subscriber, Enum eventType)
    {
        if (!eventMap.TryGetValue(eventType, out var list))
        {
            list = new();
            eventMap[eventType] = list;
        }
        list.Add(subscriber);
    }


    public void Invoke(Enum eventType, object arg = null)
    {
        if (eventMap.TryGetValue(eventType, out var list))
        {
            foreach (var subscriber in list)
            {
                if (!subscriber.IsActive) continue;
                subscriber.Callback(arg);
            }
        }
    }
}


#region Subscriber
public interface ISubscribe
{
    bool IsActive { get; }
    void Callback(object arg);
}

public sealed class Subscribe : ISubscribe
{
    private readonly MonoBehaviour owner;
    private readonly Action callback;

    public bool IsActive => owner.enabled;

    public Subscribe(MonoBehaviour owner, Action callback)
    {
        this.owner = owner;
        this.callback = callback;
    }

    public void Callback(object arg)
    {
        callback();
    }
}

public sealed class Subscribe<T> : ISubscribe
{
    private readonly MonoBehaviour owner;
    private readonly Action<T> callback;

    public bool IsActive => owner && owner.enabled;

    public Subscribe(MonoBehaviour owner, Action<T> callback)
    {
        this.owner = owner;
        this.callback = callback;
    }

    public void Callback(object arg)
    {
        if (arg is T typed)
        {
            callback(typed);
        }
    }
}
#endregion
