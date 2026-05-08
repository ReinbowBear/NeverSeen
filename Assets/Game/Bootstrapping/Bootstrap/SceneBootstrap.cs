using UnityEngine;

public class SceneBootstrap
{

    public TransitionReady? ConsumeTransition(World world)
    {
        foreach (var (transition, entity) in world.Query<TransitionReady>())
        {
            return transition;
        }

        return null;
    }


    public void OnSceneEnter(EntityCommands commands)
    {
        commands.AddOneFrame(new OnSceneEnter());
    }

    public void OnSceneExit(EntityCommands commands)
    {
        commands.AddOneFrame(new OnSceneClose());
    }


    public void SetInputs(EntityCommands commands)
    {
        var intent = new SwitchInputInent(InputMode.UI);
        commands.AddOneFrame(intent);
    }

    public void CollectEntities(World world)
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Entity"))
        {
            world.CreateEntity(obj);
        }
    }
}
