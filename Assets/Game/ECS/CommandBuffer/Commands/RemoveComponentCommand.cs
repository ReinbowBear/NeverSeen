
public class RemoveComponentCommand<T> : ICommand
{
    private Entity entity;

    private ComponentRegistry componentRegistry;
    private QueryRegistry queryRegistry;

    public RemoveComponentCommand(Entity entity,  ComponentRegistry componentRegistry, QueryRegistry queryRegistry)
    {
        this.entity = entity;

        this.componentRegistry = componentRegistry;
        this.queryRegistry = queryRegistry;
    }


    public void Execute()
    {
        var chunk = componentRegistry.GetStore<T>();
        chunk.Remove(entity);

        queryRegistry.TryRemoveEntity(entity);
    }
}
