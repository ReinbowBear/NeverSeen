
public class DestroyEntityCommand : ICommand
{
    private Entity entity;

    public DestroyEntityCommand(Entity entity)
    {
        this.entity = entity;
    }


    public void Execute(World world)
    {
        world.DestroyEntity(entity);
    }
}

public struct OnDestroyEntity
{
    
}
