
public class DestroyEntityCommand : ICommand
{
    private Entity entity;

    private EntityObjRegistry objectRegistry;
    private EntityRegistry entityRegistry;
    private TypeRegistry typeRegistry;

    private ComponentRegistry componentRegistry;
    private QueryRegistry queryRegistry;

    public DestroyEntityCommand(Entity entity,  EntityObjRegistry objectRegistry, EntityRegistry entityRegistry, TypeRegistry typeRegistry, ComponentRegistry componentRegistry, QueryRegistry queryRegistry)
    {
        this.entity = entity;

        this.objectRegistry = objectRegistry;
        this.entityRegistry = entityRegistry;
        this.typeRegistry = typeRegistry;

        this.componentRegistry = componentRegistry;
        this.queryRegistry = queryRegistry;
    }


    public void Execute()
    {
        foreach(var compIndex in entity.Mask.GetSetBits())
        {
            var type = typeRegistry.GetType(compIndex);
            var chunk = componentRegistry.GetStore(type);
            chunk.Remove(entity);
        }

        queryRegistry.RemoveEntity(entity);
        objectRegistry.RemoveEntity(entity);
        entityRegistry.RemoveEntity(entity);
    }
}