using System;
using System.Collections.Generic;

public sealed class ArchetypeRegistry
{
    private readonly Dictionary<BitMask64, Archetype> archetypes = new();
    private readonly Dictionary<Entity, EntityLocation> entityLocations = new();

    private readonly ComponentTypeRegistry typeRegistry;

    public ArchetypeRegistry(ComponentTypeRegistry typeRegistry)
    {
        this.typeRegistry = typeRegistry;
    }


    public void AddEntity(Entity entity, object[] components)
    {
        List<Type> types = ExtractTypes(components);
        SortTypes(types);

        var archetype = GetArchetype(types);

        var chunk = archetype.GetChunk();
        int slot = chunk.AddEntity(entity, components);

        entityLocations[entity] = new EntityLocation
        {
            Archetype = archetype,
            Chunk = chunk,
            IndexInChunk = slot
        };
    }

    public void RemoveEntity(Entity entity)
    {
        if (!entityLocations.TryGetValue(entity, out var location)) return;

        int slot = location.IndexInChunk;
        var chunk = location.Chunk;

        chunk.RemoveEntity(entity);

        if (slot < chunk.Entities.Count)
        {
            Entity moved = chunk.Entities[slot];

            var loc = entityLocations[moved];
            loc.IndexInChunk = slot;
            entityLocations[moved] = loc;
        }

        entityLocations.Remove(entity);
    }


    public EntityLocation GetEntityLocation(Entity entity)
    {
        if (!entityLocations.TryGetValue(entity, out var location)) throw new InvalidOperationException("Entity not found.");
        return location;
    }

    public void MoveEntity(Entity entity, object[] newComponents)
    {
        RemoveEntity(entity);
        AddEntity(entity, newComponents);
    }

    public List<Archetype> GetAllArchetypes()
    {
        return new List<Archetype>(archetypes.Values);
    }


    private Archetype GetArchetype(List<Type> types)
    {
        BitMask64 mask = BuildMask(types);
        if (archetypes.TryGetValue(mask, out var existing)) return existing;

        var archetype = new Archetype(types, mask);
        archetypes.Add(mask, archetype);
        return archetype;
    }


    private List<Type> ExtractTypes(object[] components)
    {
        var list = new List<Type>(components.Length);
        for (int i = 0; i < components.Length; i++)
        {
            list.Add(components[i].GetType());
        }
        return list;
    }

    private void SortTypes(List<Type> types)
    {
        types.Sort((a, b) =>
        {
            int indexA = typeRegistry.GetIndex(a);
            int indexB = typeRegistry.GetIndex(b);
            return indexA.CompareTo(indexB);
        });
    }

    private BitMask64 BuildMask(List<Type> types)
    {
        var mask = new BitMask64();
        foreach (var type in types)
        {
            int index = typeRegistry.GetIndex(type);
            mask.Add(index);
        }
        return mask;
    }


    public void ClearAllMasks()
    {
        foreach (var archetype in archetypes.Values)
        {
            archetype.ClearAllMasks();
        }
    }
}


public struct EntityLocation
{
    public Archetype Archetype;
    public Chunk Chunk;
    public int IndexInChunk;
}
