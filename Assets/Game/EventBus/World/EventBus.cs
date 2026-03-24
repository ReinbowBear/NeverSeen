using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{
    private Dictionary<Enum, List<IComponentCallback>> eventMap = new();

    public void AddListener(IComponentCallback subscriber, Enum eventType)
    {
        if (!eventMap.TryGetValue(eventType, out var list))
        {
            list = new();
            eventMap[eventType] = list;
        }
        list.Add(subscriber);
    }


    public void Invoke(Enum eventType, object[] arg)
    {
        if (eventMap.TryGetValue(eventType, out var list))
        {
            foreach (var subscriber in list)
            {
                subscriber.TryInvoke(arg);
            }
        }
    }
}

public interface IComponentCallback
{
    void TryInvoke(object[] args);
}
