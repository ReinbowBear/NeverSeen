using System;
using System.Collections.Generic;

public sealed class SystemRunner
{
    private readonly List<ISystem> systems = new();
    private readonly Dictionary<Type, ISystem> systemsByType = new();
    private readonly Dictionary<Type, Filter> systemFilters = new();

    private readonly Dictionary<ISystem, Type> systemTypes = new();
    private readonly Dictionary<Type, List<Filter>> filtersByComponent = new();

    private World world;

    public SystemRunner(World world)
    {
        this.world = world;
    }


    public void AddSystem<T>(T newSystem) where T : struct, ISystem
    {
        var type = typeof(T);
        if (systemFilters.ContainsKey(type)) return;

        var newFilter = world.GetFilter();

        systems.Add(newSystem);
        systemsByType[type] = newSystem;
        systemFilters[type] = newFilter;
        systemTypes[newSystem] = type;

        newSystem.SetFilter(newFilter);
        newFilter.Build();

        foreach (var filterType in newFilter.RequiredTypes)
        {
            if (!filtersByComponent.TryGetValue(filterType, out var list))
            {
                filtersByComponent[type] = list = new();
            }
            list.Add(newFilter);
        }
    }
    
    public void RemoveSystem<T>() where T : ISystem
    {
        var type = typeof(T);
        if (systemsByType.TryGetValue(type, out var system))
        {
            systems.Remove(system);
            systemsByType.Remove(type);
            systemFilters.Remove(type);
        }
    }


    public void Update()
    {
        foreach (var system in systems)
        {
            var filter = systemFilters[systemTypes[system]];
            if (filter.Entities.Count == 0) continue;
            system.Update(world, filter);
        }
    }


    public void AddEntityToFilters(Entity entity)
    {
        foreach (var filterList in filtersByComponent.Values)
        {
            foreach (var filter in filterList)
            {
                filter.UpdateEntity(entity);
            }
        }
    }

    public void UpdateFilters(Entity entity, Type componentType)
    {
        if (!filtersByComponent.TryGetValue(componentType, out var filters)) return;

        foreach (var filter in filters)
        {
            filter.UpdateEntity(entity);
        }
    }

    public void RemoveEntityFromFilters(Entity entity)
    {
        foreach (var filterList in filtersByComponent.Values)
        {
            foreach (var filter in filterList)
            {
                filter.RemoveEntity(entity);
            }
        }
    }
}
