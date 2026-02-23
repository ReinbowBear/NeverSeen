using System;
using UnityEngine;

public class EventWorld : IService
{
    private ComponentCache componentCache = new();
    private EventBus eventBus = new();


    #region AddListener
    public void AddListener(MonoBehaviour mono, Action callback, Enum eventType)
    {
        var subscribe = new Subscribe(mono, callback);
        eventBus.AddListener(subscribe, eventType);
    }

    public void AddListener<T>(MonoBehaviour mono, Action<T> callback, Enum eventType)
    {
        var subscribe = new Subscribe<T>(mono, callback);
        eventBus.AddListener(subscribe, eventType);
    }
    #endregion


    #region Invoke
    public void Invoke(Enum eventType)
    {
        eventBus.Invoke(eventType);
    }

    public void Invoke(object data, Enum eventType)
    {
        eventBus.Invoke(eventType, data);
    }

    public void Invoke(GameObject obj, Enum eventType)
    {
        var components = componentCache.GetComponents(obj);
        foreach (var comp in components)
        {
            eventBus.Invoke(eventType, comp);
        }
    }
    #endregion

    #region ComponentCache
    public void AddEntity(GameObject obj)
    {
        componentCache.AddEntity(obj);
    }

    public void DestroyEntity(GameObject obj)
    {
        componentCache.RemoveEntity(obj);
        GameObject.Destroy(obj);
    }

    public void AddComponent<T>(GameObject obj) where T : MonoBehaviour
    {
        obj.AddComponent<T>();
        componentCache.RefreshEntity(obj);
    }

    public void RemoveComponent<T>(GameObject obj) where T : MonoBehaviour
    {
        GameObject.Destroy(obj.GetComponent<T>());
        componentCache.RefreshEntity(obj);
    }
    #endregion
}

public interface IEventSender
{
    void SetEventBus(EventWorld eventWorld);
}
