using System;
using UnityEngine;

public class World
{
    private EntityObjRegistry objectRegistry;
    private EntityRegistry entityRegistry;
    private TypeRegistry typeRegistry;

    private ComponentRegistry componentRegistry;
    private CommandBuffer commandBuffer;

    private QueryRegistry queryRegistry;

    public World()
    {
        objectRegistry = new();
        entityRegistry = new();
        typeRegistry = new();
    
        componentRegistry = new(typeRegistry);
        commandBuffer = new();

        queryRegistry = new(componentRegistry);
    }


    #region CreateEntity
    public Entity CreateEntity(GameObject obj)
    {
        var entity = entityRegistry.CreateEntity();
        commandBuffer.OnStartFrame(new CreateEntityCommand(entity, obj,  objectRegistry, componentRegistry, queryRegistry));
        return entity;
    }

    public void DestroyEntity(Entity entity)
    {
        commandBuffer.OnStartFrame(new DestroyEntityCommand(entity,  objectRegistry, entityRegistry, typeRegistry, componentRegistry, queryRegistry));
    }
    #endregion


    #region AddComponent
    public void AddOneFrame<T>() where T : struct
    {
        var entity = entityRegistry.CreateEntity();
        commandBuffer.OnStartFrame(new AddComponentCommand<T>(entity, new T(),  componentRegistry, queryRegistry));
        commandBuffer.OnEndFrame(new DestroyEntityCommand(entity,  objectRegistry, entityRegistry, typeRegistry, componentRegistry, queryRegistry));
    }

    public void AddOneFrame<T>(Entity entity) where T : struct
    {
        commandBuffer.OnStartFrame(new AddComponentCommand<T>(entity, new T(),  componentRegistry, queryRegistry));
        commandBuffer.OnEndFrame(new RemoveComponentCommand<T>(entity,  componentRegistry, queryRegistry));
    }


    public void AddComponent<T>(Entity entity, T component)
    {
        commandBuffer.OnStartFrame(new AddComponentCommand<T>(entity, component,  componentRegistry, queryRegistry));
    }

    public void RemoveComponent<T>(Entity entity) // если у сущности 0 компонентов она не удаляется кстати
    {
        commandBuffer.OnStartFrame(new RemoveComponentCommand<T>(entity,  componentRegistry, queryRegistry));
    }
    #endregion


    #region Buffer
    public void ExecuteCommands()
    {
        commandBuffer.ExecuteCommands();
    }

    public void ExecuteAfterFrame()
    {
        commandBuffer.ExecuteAfterFrame();
    }
    #endregion


    #region Get
    public bool HasEntityWith(Type type)
    {
        return componentRegistry.HasEntityWith(type);
    }

    public T GetComponent<T>(Entity entity)
    {
        var chunk = componentRegistry.GetStore<T>();
        return chunk.GetComponent(entity);
    }

    public Entity GetEntity(GameObject obj)
    {
        return objectRegistry.GetEntity(obj);
    }

    public GameObject GetObject(Entity entity)
    {
        return objectRegistry.GetObject(entity);
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

    public Query<T1, T2 ,T3, T4> Query<T1, T2, T3, T4>()
    {
        var description = new QueryDescription(typeRegistry);
        var newQuery = new Query<T1, T2, T3, T4>(queryRegistry, description);

        newQuery.Require<T1>();
        newQuery.Require<T2>();
        newQuery.Require<T2>();
        return newQuery;
    }
    #endregion


    public void Clear()
    {
        objectRegistry.Clear();
        entityRegistry.Clear();

        componentRegistry.Clear();
        commandBuffer.Clear();

        queryRegistry.Clear();
    }
}
