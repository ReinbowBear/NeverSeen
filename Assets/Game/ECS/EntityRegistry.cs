using System.Collections.Generic;

public sealed class EntityRegistry
{
    private readonly Dictionary<int, BitMask> entityMasks = new();
    private readonly HashSet<Entity> entities = new();
    private int nextEntityId = 0;

    public Entity CreateEntity()
    {
        var entity = new Entity(nextEntityId++);
        entities.Add(entity);
        entityMasks[entity.Id] = new BitMask();
        return entity;
    }

    public void DestroyEntity(Entity entity)
    {
        entities.Remove(entity);
        entityMasks.Remove(entity.Id);
    }


    public bool Exists(Entity entity)
    {
        return entities.Contains(entity);
    }

    public IEnumerable<Entity> GetAllEntities()
    {
        return entities;
    }


    public BitMask GetMask(Entity entity) // EntityRegistry помимо сущностей хранит их битовые маски компонентов
    {
        return entityMasks[entity.Id]; // осторожно! не возращаем ref
    }


    public void AddComponentBit(Entity entity, int bitIndex)
    {
        var mask = entityMasks[entity.Id];
        mask.Set(bitIndex, true);
        entityMasks[entity.Id] = mask;
    }

    public void RemoveComponentBit(Entity entity, int bitIndex)
    {
        var mask = entityMasks[entity.Id];
        mask.Set(bitIndex, false);
        entityMasks[entity.Id] = mask;
    }
}
