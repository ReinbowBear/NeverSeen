using System;
using UnityEngine;

public class EventWorld
{
    private ComponentCache componentCache = new();
    private ComponentBus componentBus = new();
    private EventBus eventBus = new();
    
    #region DataBus
    public void AddListener<T>(Action<T> callback, Enum eventType) where T : Component
    {
        componentBus.AddListener(callback, eventType);
    }

    public void RemoveListener<T>(Action<T> callback, Enum eventType) where T : Component
    {
        componentBus.RemoveListener(callback, eventType);
    }

    public void Invoke<TEvent>(GameObject obj, TEvent eventType) where TEvent : Enum
    {
        var components = componentCache.GetComponents(obj);
        foreach (var comp in components)
        {
            componentBus.Invoke(comp, eventType);
        }
    }
    #endregion

    #region  EventBus
    public void AddListener(Action callback, Enum eventType)
    {
        eventBus.AddListener(callback, eventType);
    }

    public void RemoveListener(Action callback, Enum eventType)
    {
        eventBus.RemoveListener(callback, eventType);
    }

    public void Invoke(Enum eventType)
    {
        eventBus.Invoke(eventType);
    }
    #endregion

    #region ComponentCache
    public void AddEntity(GameObject obj)
    {
        componentCache.AddEntity(obj);
    }

    public void RemoveEntity(GameObject obj)
    {
        componentCache.RemoveEntity(obj);
    }

    public void RefreshEntity(GameObject obj)
    {
        componentCache.RefreshEntity(obj);
    }
    #endregion
}

public interface IEventSender
{
    void SetEventBus(EventWorld eventWorld);
}
