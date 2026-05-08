
public class AddComponentCommand<T> : ICommand
{
    public Entity entity;
    public T component;

    public AddComponentCommand(Entity entity, T component)
    {
        this.entity = entity;
        this.component = component;
    }

    public void Execute(World world)
    {
        world.AddComponent(entity, component);
    }
}

public struct OnAddComponent<T>
{
    
}
