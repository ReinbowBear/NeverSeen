using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityContext
{
    public GameObject entityObject;
    private Dictionary<Type, IComponentData> components = new();

    public void AddComponent<T>(T component) where T : IComponentData
    {
        components[typeof(T)] = component;
    }

    public void RemoveComponent<T>() where T : IComponentData
    {
        components.Remove(typeof(T));
    }


    public bool HasComponent<T>() where T : IComponentData
    {
        return components.ContainsKey(typeof(T));
    }

    public T GetComponent<T>() where T : IComponentData
    {
        components.TryGetValue(typeof(T), out var data);
        return (T)data;
    }
}
