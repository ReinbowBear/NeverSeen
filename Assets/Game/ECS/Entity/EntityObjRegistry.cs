using System.Collections.Generic;
using UnityEngine;

public class EntityObjRegistry
{
    private Dictionary<Entity, GameObject> entityToObj = new();
    private Dictionary<GameObject, Entity> objToEntity = new();

    public void AddEntity(Entity entity, GameObject obj)
    {
        entityToObj[entity] = obj;
        objToEntity[obj] = entity;
    }

    public void RemoveEntity(Entity entity)
    {
        if (!entityToObj.ContainsKey(entity)) return;

        objToEntity.Remove(entityToObj[entity]);
        entityToObj.Remove(entity);
    }


    public Entity GetEntity(GameObject obj)
    {
        return objToEntity[obj];
    }

    public GameObject GetObject(Entity entity)
    {
        return entityToObj[entity];
    }


    public void Clear()
    {
        entityToObj.Clear();
        objToEntity.Clear();
    }
}
