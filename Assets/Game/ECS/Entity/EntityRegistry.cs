
public class EntityRegistry
{
    private SparseSet<Entity> entities = new();
    private int nextEntityId = 1;

    public Entity CreateEntity()
    {
        int id = nextEntityId++;

        var newEntity = new Entity(id);
        entities.Add(newEntity);

        return newEntity;
    }

    public void RemoveEntity(Entity entity)
    {
        entities.Remove(entity);
    }
}
