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
        entity = world.CreateEntity();

        var authorings = GetComponents<IAuthoring>();
        foreach (var autho in authorings)
        {
            autho.Bake(entity, world);
        }
    }


    void OnDestroy()
    {
        world.DestroyEntity(entity);
    }
}
