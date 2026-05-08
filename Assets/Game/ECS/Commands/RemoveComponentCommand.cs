
public class RemoveComponentCommand<T> : ICommand
{
    private Entity entity;

    public RemoveComponentCommand(Entity entity)
    {
        this.entity = entity;
    }


    public void Execute(World world)
    {
        world.RemoveComponent<T>(entity);
    }
}

public struct OnRemoveComponent<T>
{
    
}
