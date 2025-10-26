using System;
using System.Collections.Generic;

public sealed class Filter
{
    private readonly EntityRegistry entityRegistry;
    private readonly TypeRegistry typeRegistry;

    private readonly BitMask requiredMask = new();
    private readonly BitMask excludedMask = new();

    private readonly HashSet<Type> requiredTypes = new();
    private readonly HashSet<Type> excludedTypes = new();
    public IReadOnlyCollection<Type> RequiredTypes => requiredTypes;
    public IReadOnlyCollection<Type> ExcludedTypes => excludedTypes;

    private readonly HashSet<Entity> entitySet = new(); // хеш сет для проверок, лист для быстрых итераций
    private readonly List<Entity> entities = new();

    public IReadOnlyList<Entity> Entities => entities;

    public Filter(EntityRegistry entityRegistry, TypeRegistry typeRegistry)
    {
        this.entityRegistry = entityRegistry;
        this.typeRegistry = typeRegistry;
    }


    public Filter Require<T>() where T : struct, IComponentData
    {
        requiredMask.Set(typeRegistry.GetIndex<T>(), true);
        requiredTypes.Add(typeof(T));
        return this;
    }

    public Filter Exclude<T>() where T : struct, IComponentData
    {
        excludedMask.Set(typeRegistry.GetIndex<T>(), true);
        excludedTypes.Add(typeof(T));
        return this;
    }


    public void Build()
    {
        entitySet.Clear();
        entities.Clear();

        foreach (var entity in entityRegistry.GetAllEntities())
        {
            var mask = entityRegistry.GetMask(entity);
            if (mask.Matches(requiredMask) && !mask.Matches(excludedMask))
            {
                AddEntity(entity);
            }
        }
    }


    public void UpdateEntity(Entity entity)
    {
        var mask = entityRegistry.GetMask(entity);
        bool matches = mask.Matches(requiredMask) && !mask.Matches(excludedMask);

        if (matches)
        {
            AddEntity(entity);
        }
        else
        {
            RemoveEntity(entity);
        }
    }

    private void AddEntity(Entity e)
    {
        if (entitySet.Add(e))
        {
            entities.Add(e);
        }
    }

    public void RemoveEntity(Entity e)
    {
        if (entitySet.Remove(e))
        {
            entities.Remove(e);
        }
    }
}
