using System;
using System.Collections.Generic;

public sealed class FilterRegistry
{
    private readonly Dictionary<Type, Filter> filters = new();
    private readonly CompTypeRegistry typeRegistry;
    private readonly EntityRegistry entityRegistry;

    public FilterRegistry(EntityRegistry entityRegistry, CompTypeRegistry typeRegistry)
    {
        this.entityRegistry = entityRegistry;
        this.typeRegistry = typeRegistry;
    }


    public void AddFilter<T>(T system) where T : ISystem
    {
        var filter = new Filter(entityRegistry, typeRegistry);
        system.SetFilter(filter);
        filters[typeof(T)] = filter;
        filter.Build();
    }

    public void RemoveFilter(Type systemType)
    {
        filters.Remove(systemType);
    }


    public Filter GetFilter(Type systemType)
    {
        return filters[systemType];
    }


    public void UpdateFilters(Entity entity)
    {
        foreach (var filter in filters.Values)
        {
            filter.UpdateEntity(entity);
        }
    }

    public void RemoveEntity(Entity entity)
    {
        foreach (var filter in filters.Values)
        {
            filter.RemoveEntity(entity);
        }
    }
}
