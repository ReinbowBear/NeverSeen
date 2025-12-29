using System.Collections.Generic;
using UnityEngine;

public class EntityActor : MonoBehaviour
{
    private Entity entity;
    private World world;

    public void Init(World world)
    {
        this.world = world;
        CreateEntity();
    }


    private void CreateEntity()
    {
        var authorings = GetComponents<IActor>();
        List<object> components = new List<object>();

        foreach (var authoring in authorings)
        {
            var component = authoring.GetComponentData();
            components.Add(component);
        }

        entity = world.CreateEntity(components.ToArray());
    }


    void OnDestroy()
    {
        world.DestroyEntity(entity);
    }
}


public struct EntityObject : IComponentData
{
    public GameObject gameObject;
}
