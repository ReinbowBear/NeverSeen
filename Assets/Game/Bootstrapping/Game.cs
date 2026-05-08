using UnityEngine;

public class Game
{
    private GroupBuilder groupBuilder = new();
    private SceneBootstrap sceneBootstraper = new();
    private WorldRunner runner = new();


    public void Update()
    {
        runner.Update();

        var transition = sceneBootstraper.ConsumeTransition(runner.World);
        if (transition != null) DoTransition(transition.Value.To);
    }


    public void DoTransition(string request)
    {
        groupBuilder.ClearContainer();

        var composite = groupBuilder.GetGroupsForScene(request);

        foreach (var desc in composite.Descriptors)
        {
            var groupDesc = new SystemGroupDesc(desc);
            var group = groupBuilder.BuildSystems(groupDesc);
            runner.AddGroup(group);
        }

        sceneBootstraper.CollectEntities(runner.World);
        sceneBootstraper.OnSceneEnter(runner.Commands);
    
        sceneBootstraper.SetInputs(runner.Commands);
        runner.Buffer.ExecuteCommands(runner.World);
        Debug.Log("костыльный Set инпута");
    }
}
