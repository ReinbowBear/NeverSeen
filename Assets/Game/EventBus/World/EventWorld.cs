using System;
using UnityEngine;

[Service]
public class EventWorld
{
    private ComponentCache componentCache = new();
    private EventBus eventBus = new();
    private object[] components = new object[1];


    #region Listener
    public void AddListener(Action callback, Enum eventType)
    {
        var subscribe = new Callback(callback);
        eventBus.AddListener(subscribe, eventType);
    }

    public void AddListener<T1>(Action<T1> callback, Enum eventType)
    {
        var subscribe = new Callback<T1>(callback);
        eventBus.AddListener(subscribe, eventType);
    }

    public void AddListener<T1, T2>(Action<T1, T2> callback, Enum eventType)
    {
        var subscribe = new Callback<T1, T2>(callback);
        eventBus.AddListener(subscribe, eventType);
    }

    public void AddListener<T1, T2, T3>(Action<T1, T2, T3> callback, Enum eventType)
    {
        var subscribe = new Callback<T1, T2, T3>(callback);
        eventBus.AddListener(subscribe, eventType);
    }
    #endregion


    #region Invoke
    public void Invoke(Enum eventType)
    {
        components[0] = null;
        eventBus.Invoke(eventType, components);
    }

    public void Invoke(object data, Enum eventType)
    {
        components[0] = data;
        eventBus.Invoke(eventType, components);
    }

    public void Invoke(GameObject obj, Enum eventType)
    {
        var components = componentCache.GetComponents(obj);
        eventBus.Invoke(eventType, components);
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
