using System;
using System.Collections.Generic;

public sealed class World
{
    private EntityRegistry entityRegistry;
    private ComponentTypeRegistry typeRegistry;
    private ArchetypeRegistry archetypeRegistry;
    private QueryRegistry queryRegistry;

    public void Init()
    {
        entityRegistry = new();
        typeRegistry = new();
        archetypeRegistry = new(typeRegistry); // typeRegistry нужен только для построения масок 
        queryRegistry = new(archetypeRegistry); // нужен только для создания запросов
    }


    #region CreateEntity
    public Entity CreateEntity(object[] components)
    {
        var entity = entityRegistry.CreateEntity();
        archetypeRegistry.AddEntity(entity, components);
        return entity;
    }

    public void DestroyEntity(Entity entity)
    {
        entityRegistry.RemoveEntity(entity);
        archetypeRegistry.RemoveEntity(entity);
    }
    #endregion

    #region AddComponent
    public void AddComponent<T>(Entity entity, T newComponent) where T : struct, IComponentData
    {
        var loc = archetypeRegistry.GetEntityLocation(entity);
        var chunk = loc.Chunk;
        int index = loc.IndexInChunk;

        var oldComponents = chunk.GetEntityComponents(index);
        var newComponents = new object[oldComponents.Length + 1];

        Array.Copy(oldComponents, newComponents, oldComponents.Length);
        newComponents[^1] = newComponent;

        archetypeRegistry.MoveEntity(entity, newComponents);
    }

    public void RemoveComponent<T>(Entity entity) where T : struct, IComponentData
    {
        var loc = archetypeRegistry.GetEntityLocation(entity);
        var chunk = loc.Chunk;
        int index = loc.IndexInChunk;

        var oldComponents = chunk.GetEntityComponents(index);
        var list = new List<object>(oldComponents.Length);
        var removeType = typeof(T);

        foreach (var component in oldComponents) if (component.GetType() != removeType) list.Add(component);

        archetypeRegistry.MoveEntity(entity, list.ToArray());
    }
    #endregion

    #region GetComponent
    public ref T GetRO<T>(Entity entity) where T : struct, IComponentData
    {
        var loc = archetypeRegistry.GetEntityLocation(entity);
        var chunk = loc.Chunk;
        int slot = loc.IndexInChunk;

        int typeIndex = typeRegistry.GetIndex<T>();
        int localIndex = loc.Archetype.componentToLocal[typeIndex];

        return ref chunk.GetComponentRO<T>(slot, localIndex);
    }

    public ref T GetRW<T>(Entity entity) where T : struct, IComponentData
    {
        var loc = archetypeRegistry.GetEntityLocation(entity);
        var chunk = loc.Chunk;
        int slot = loc.IndexInChunk;

        int typeIndex = typeRegistry.GetIndex<T>();
        int localIndex = loc.Archetype.componentToLocal[typeIndex];
        return ref chunk.GetComponentRW<T>(slot, localIndex);
    }
    #endregion

    #region System
    public void AddSystem(ISystem system)
    {
        // Здесь будет SystemRunner
        // queryManager может быть уведомлен для фильтров
    }

    public void RemoveSystem(ISystem system)
    {
        // systemRunner.RemoveSystem(system);
    }

    public void Update()
    {
        // systemRunner.Update();
    }
    #endregion

    #region Query
    public QueryBuilder Query<T1, T2>() where T1 : struct, IComponentData where T2 : struct, IComponentData
    {
        var builder = new QueryBuilder(typeRegistry);
        builder.WithRead<T1>();
        builder.WithRead<T2>();
        return builder;
    }

    //public Query<T1, T2> Query<T1, T2>()
    //where T1 : struct, IComponentData
    //where T2 : struct, IComponentData
    //{
    //    var builder = new QueryBuilder(typeRegistry, queryRegistry);
    //    builder.WithRead<T1>();
    //    builder.WithRead<T2>();
    //
    //    return new Query<T1, T2>(builder.Build());
    //}

    #endregion
}
