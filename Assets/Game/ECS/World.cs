
public sealed class World
{
    private readonly EntityRegistry entityRegistry;
    private readonly ComponentRegistry componentRegistry;
    private readonly TypeRegistry typeRegistry;
    private readonly SystemRunner systemRunner;

    public World(EntityRegistry entityRegistry, ComponentRegistry componentRegistry, TypeRegistry typeRegistry, SystemRunner systemRunner)
    {
        this.entityRegistry = entityRegistry;
        this.componentRegistry = componentRegistry;
        this.typeRegistry = typeRegistry;
        this.systemRunner = systemRunner;
    }

    #region CreateEntity
    public Entity CreateEntity()
    {
        var entity = entityRegistry.CreateEntity();
        systemRunner.AddEntityToFilters(entity);
        return entity;
    }

    public void DestroyEntity(Entity entity)
    {
        if (!entityRegistry.Exists(entity)) return;

        componentRegistry.RemoveAllComponents(ref entity);
        entityRegistry.DestroyEntity(entity);
        systemRunner.RemoveEntityFromFilters(entity);
    }
    #endregion

    #region AddComponent
    public void AddComponent<T>(Entity entity, T component) where T : struct, IComponentData
    {
        int bit = typeRegistry.GetIndex<T>();
        entityRegistry.AddComponentBit(entity, bit);
        componentRegistry.AddComponent(ref entity, component);
        systemRunner.UpdateFilters(entity, typeof(T));
    }

    public void RemoveComponent<T>(Entity entity) where T : struct, IComponentData
    {
        if (!componentRegistry.HasComponent<T>(entity)) return;

        componentRegistry.RemoveComponent<T>(ref entity);
        int bit = typeRegistry.GetIndex<T>();
        entityRegistry.RemoveComponentBit(entity, bit);
        systemRunner.UpdateFilters(entity, typeof(T));
    }
    #endregion

    #region GetComponent
    public bool HasComponent<T>(Entity entity) where T : struct, IComponentData
    {
        return componentRegistry.HasComponent<T>(entity);
    }

    public ref T GetComponent<T>(Entity entity) where T : struct, IComponentData
    {
        return ref componentRegistry.GetComponent<T>(entity);
    }
    #endregion

    #region System
    public void AddSystem<T>(T system) where T : struct, ISystem
    {
        systemRunner.AddSystem(system);
    }

    public void RemoveSystem<T>() where T : struct, ISystem
    {
        systemRunner.RemoveSystem<T>();
    }

    public void Update()
    {
        systemRunner.Update();
    }
    #endregion

    #region Utility
    public Filter GetFilter()
    {
        return new Filter(entityRegistry, typeRegistry);
    }
    #endregion
}
