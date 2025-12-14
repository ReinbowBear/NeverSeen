using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EntityAuthoring : MonoBehaviour
{
    private Entity entity;
    [Inject] private World world;

    void Start()
    {
        CreateEntity();
    }


    private void CreateEntity()
    {
        var authorings = GetComponents<IAuthoring>();
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
