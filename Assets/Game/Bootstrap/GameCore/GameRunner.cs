using UnityEngine;

public class GameRunner
{
    private SceneRunner SceneRunner = new();
    private World world = new();
    
    public void AddGroup(SystemGroup systemGroup)
    {
        SceneRunner.AddGroup(systemGroup);
        BuildSubsQuery(systemGroup);
    }

    public void RemoveGroup(SystemGroup systemGroup)
    {
        SceneRunner.RemoveGroup(systemGroup);
    }


    public void UpdateWorld()
    {
        SceneRunner.Update(world);
    }


    public TransitionReady? ConsumeTransition()
    {
        foreach (var transition in world.Query<TransitionReady>())
        {
            return transition;
        }

        return null;
    }

    public void EnterSceneEvent()
    {
        world.AddOneFrame<OnSceneEnter>();
    }

    public void ExitSceneEvent()
    {
        world.AddOneFrame<OnSceneClose>();
    }


    private void BuildSubsQuery(SystemGroup systemGroup)
    {
        foreach (var phase in systemGroup.Phases)
        {
            foreach (var sustemSub in phase.SystemSubs)
            {
                foreach (var sub in sustemSub.Subscribes)
                {
                    sub.BuildQuery(world);
                }
            }
        }
    }

    public void CollectEntitys()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Entity"))
        {
            world.CreateEntity(obj);
        }

        world.ExecuteCommands();
    }


    public void Clear()
    {
        SceneRunner.ClearGroups();
        world.Clear();
    }
}
