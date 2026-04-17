
public class AddComponentCommand<T> : ICommand
{
    private Entity entity;
    private T component;

    private ComponentRegistry componentRegistry;
    private QueryRegistry queryRegistry;

    public AddComponentCommand(Entity entity, T component,  ComponentRegistry componentRegistry, QueryRegistry queryRegistry)
    {
        this.entity = entity;
        this.component = component;

        this.componentRegistry = componentRegistry;
        this.queryRegistry = queryRegistry;
    }


    public void Execute()
    {
        var chunk = componentRegistry.GetStore<T>();
        chunk.Add(entity, component);

        queryRegistry.TryAddEntity(entity);
    }
}
