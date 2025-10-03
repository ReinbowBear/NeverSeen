using System.Collections.Generic;
using UnityEngine;

public class World
{
    private List<Entity> Entitys = new();
    public Entity ChosenEntity;

    public void AddEntity(GameObject entityObj)
    {
        if (entityObj.TryGetComponent<Entity>(out var component))
        {
            Entitys.Add(component);
        }
    }


    public List<T> GetEntityWithComponents<T>()
    {
        List<T> componentList = new();
        foreach (var entity in Entitys)
        {
            if (entity.TryGetComponent<T>(out var component))
            {
                componentList.Add(component);
            }
        }
        return componentList;
    }


    public void DeleteEntity(GameObject entityObj)
    {
        if (entityObj.TryGetComponent<Entity>(out var component))
        {
            component.Selected(false);
            Entitys.Remove(component);
        }
    }
}
