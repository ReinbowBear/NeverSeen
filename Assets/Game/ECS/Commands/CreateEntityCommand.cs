using UnityEngine;

public class CreateEntityCommand : ICommand
{
    private GameObject obj;

    public CreateEntityCommand(GameObject obj)
    {
        this.obj = obj;
    }


    public void Execute(World world)
    {
        world.CreateEntity(obj);
    }
}

public struct OnCreateEntity
{
    
}
