using System.Collections.Generic;
using UnityEngine;

public class ComponentCache
{
    private Dictionary<GameObject, Component[]> componentsCache = new();

    public void AddEntity(GameObject obj)
    {
        var components = obj.GetComponents<Component>();
        componentsCache[obj] = components;
    }

    public void RemoveEntity(GameObject obj)
    {
        componentsCache.Remove(obj);
    }

    public void RefreshEntity(GameObject obj)
    {
        RemoveEntity(obj);
        AddEntity(obj);
    }


    public Component[] GetComponents(GameObject obj)
    {
        if (!componentsCache.TryGetValue(obj, out var components))
        {
            AddEntity(obj);
            components = componentsCache[obj];
        }

        return components;
    }
}
