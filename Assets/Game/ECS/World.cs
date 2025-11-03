
public sealed class World
{
    private readonly EntityRegistry entityRegistry;
    private readonly ComponentRegistry componentRegistry;
    private readonly CompTypeRegistry typeRegistry;
    private readonly SystemRunner systemRunner;
    private readonly FilterRegistry filterRegistry;

    public World(EntityRegistry entityRegistry, ComponentRegistry componentRegistry, CompTypeRegistry typeRegistry, SystemRunner systemRunner, FilterRegistry filterRegistry)
    {
        this.entityRegistry = entityRegistry;
        this.componentRegistry = componentRegistry;
        this.typeRegistry = typeRegistry;
        this.systemRunner = systemRunner;
        this.filterRegistry = filterRegistry;
    }

    #region CreateEntity
    public Entity CreateEntity()
    {
        var entity = entityRegistry.CreateEntity();
        filterRegistry.UpdateFilters(entity);
        return entity;
    }

    public void DestroyEntity(Entity entity)
    {
        if (!entityRegistry.Exists(entity)) return;

        componentRegistry.RemoveAllComponents(ref entity);
        entityRegistry.DestroyEntity(entity);
        filterRegistry.RemoveEntity(entity);
    }
    #endregion

    #region AddComponent
    public void AddComponent<T>(Entity entity, T component) where T : struct, IComponentData
    {
        if (componentRegistry.HasComponent<T>(entity)) return;

        int bit = typeRegistry.GetIndex<T>();
        entityRegistry.AddComponentBit(entity, bit);
        componentRegistry.AddComponent(ref entity, component);
        filterRegistry.UpdateFilters(entity);
    }

    public void RemoveComponent<T>(Entity entity) where T : struct, IComponentData
    {
        if (!componentRegistry.HasComponent<T>(entity)) return;

        componentRegistry.RemoveComponent<T>(ref entity);
        int bit = typeRegistry.GetIndex<T>();
        entityRegistry.RemoveComponentBit(entity, bit);
        filterRegistry.UpdateFilters(entity);
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
    public void AddSystem(ISystem system)
    {
        systemRunner.AddSystem(system);
        filterRegistry.AddFilter(system);
    }

    public void RemoveSystem(ISystem system)
    {
        systemRunner.RemoveSystem(system);
        filterRegistry.AddFilter(system);
    }

    public void Update()
    {
        systemRunner.Update();
    }
    #endregion
}
