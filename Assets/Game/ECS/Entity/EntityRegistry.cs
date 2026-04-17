using System.Collections.Generic;

public class EntityRegistry
{
    private SparseSet<Entity> entities = new();
    private Stack<int> freeIds = new();
    private int nextEntityId = 1;


    public Entity CreateEntity()
    {
        int id;

        if (freeIds.Count > 0) id = freeIds.Pop();
        else id = nextEntityId++;
    

        var entity = new Entity(id);
        entities.Add(entity);

        return entity;
    }

    public void RemoveEntity(Entity entity)
    {
        if (!entities.Contains(entity)) return;

        entities.Remove(entity);
        freeIds.Push(entity.Id);
    }


    public void Clear()
    {
        entities.Clear();
        freeIds.Clear();

        nextEntityId = 1;
    }
}
