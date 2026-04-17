using UnityEngine;

public struct EntityCommands
{
    private World world;
    private Entity entity;

    public EntityCommands(World world, Entity entity)
    {
        this.world = world;
        this.entity = entity;
    }


    public Entity CreateEntity(GameObject obj)
    {
        return world.CreateEntity(obj);
    }

    public void DestroyEntity(Entity entity)
    {
        world.DestroyEntity(entity);
    }

    public void InvokeOneFrame<T>() where T : struct => world.AddOneFrame<T>();
    public void AddOneFrame<T>() where T : struct => world.AddOneFrame<T>(entity);


    public void AddComponent<T>(T component) => world.AddComponent(entity, component);
    public void RemoveComponent<T>() => world.RemoveComponent<T>(entity);
}
