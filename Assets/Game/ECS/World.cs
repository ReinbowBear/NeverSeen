
public class World
{
    private EntityRegistry entityRegistry;
    private TypeRegistry typeRegistry;
    private ComponentRegistry componentRegistry;
    private QueryRegistry queryRegistry;
    private EntityCommandBuffer commandBuffer;

    public World()
    {
        entityRegistry = new();

        typeRegistry = new();
        componentRegistry = new();

        queryRegistry = new(componentRegistry);
        commandBuffer = new();
    }


    #region CreateEntity
    public void CreateEntity(params object[] components) // все команд баффер функции не помечают компонент как изменённый, системы могут его не видеть
    {
        commandBuffer.Add(new Command(() => CreateEntityCommand(components)));
    }

    private Entity CreateEntityCommand(params object[] components)
    {
        var entity = entityRegistry.CreateEntity();

        foreach(var comp in components)
        {
            var chunk = componentRegistry.GetStore(comp.GetType());
            chunk.Add(entity, comp);
        }
    
        return entity;
    }


    public void DestroyEntity(Entity entity)
    {
        commandBuffer.Add(new Command(() => DestroyEntityCommand(entity)));
    }

    private void DestroyEntityCommand(Entity entity)
    {
        foreach(var compIndex in entity.Mask.GetSetBits())
        {
            var type = typeRegistry.GetType(compIndex);
            var chunk = componentRegistry.GetStore(type);
            chunk.Remove(entity);
        }

        queryRegistry.RemoveEntity(entity);
        entityRegistry.RemoveEntity(entity);
    }
    #endregion


    #region AddComponent
    public void AddComponent<T>(Entity entity, T component)
    {
        commandBuffer.Add(new Command(() => AddComponentCommand(entity, component)));
    }

    private void AddComponentCommand<T>(Entity entity, T component)
    {
        var chunk = componentRegistry.GetStore<T>();
        chunk.Add(entity, component);

        queryRegistry.TryAddEntity(entity);
    }


    public void DestroyEntity<T>(Entity entity)
    {
        commandBuffer.Add(new Command(() => RemoveComponentCommand<T>(entity)));
    }

    private void RemoveComponentCommand<T>(Entity entity)
    {
        var chunk = componentRegistry.GetStore<T>();
        chunk.Remove(entity);

        queryRegistry.TryRemoveEntity(entity);
    }
    #endregion


    #region GetComponent
    public T GetRO<T>(Entity entity)
    {
        var chunk = componentRegistry.GetStore<T>();
        return chunk.GetRO(entity);
    }

    public T GetRW<T>(Entity entity)
    {
        var chunk = componentRegistry.GetStore<T>();
        return chunk.GetRW(entity);
    }
    #endregion


    #region Query
    public Query<T1> Query<T1>()
    {
        var description = new QueryDescription(typeRegistry);
        var newQuery = new Query<T1>(queryRegistry, description);

        newQuery.Require<T1>();
        return newQuery;
    }

    public Query<T1, T2> Query<T1, T2>()
    {
        var description = new QueryDescription(typeRegistry);
        var newQuery = new Query<T1, T2>(queryRegistry, description);

        newQuery.Require<T1>();
        newQuery.Require<T2>();
        return newQuery;
    }

    public Query<T1, T2 ,T3> Query<T1, T2, T3>()
    {
        var description = new QueryDescription(typeRegistry);
        var newQuery = new Query<T1, T2, T3>(queryRegistry, description);

        newQuery.Require<T1>();
        newQuery.Require<T2>(); // если вызвать Require из системы, это учитывается при фильтре архетипов
        newQuery.Require<T2>(); // но деконструктор будет лишь на Т компонентов (своего рода встроенный фильтр на HasComponent)
        return newQuery;
    }
    #endregion

    public void Clear()
    {
        componentRegistry.Clear();
    }
}
