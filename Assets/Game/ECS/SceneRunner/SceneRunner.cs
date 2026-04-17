using System.Collections.Generic;

public class SceneRunner
{
    private List<SystemGroup> groups = new();
    public SystemGroup LastGroup => groups[groups.Count - 1];


    public void AddGroup(SystemGroup systemGroup) // нет защиты от двойного исполнения систем, если те дублируются в групах
    {
        groups.Add(systemGroup);
    }

    public void RemoveGroup(SystemGroup systemGroup)
    {
        groups.Remove(systemGroup);
    }


    public void Update(World world)
    {
        foreach (var group in groups)
        {
            group.Update(world, UpdatePhase.Init);
        }

        world.ExecuteCommands();

        foreach (var group in groups)
        {
            group.Update(world, UpdatePhase.Logic);
        }

        foreach (var group in groups)
        {
            group.Update(world, UpdatePhase.View);
        }

        world.ExecuteAfterFrame();
    }


    public void ClearGroups()
    {
        groups.Clear();
    }
}
