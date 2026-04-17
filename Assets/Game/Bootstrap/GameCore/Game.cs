
public class Game
{
    private GameBuilder gameBuilder = new();
    private GameRunner gameRunner = new();


    public void Update()
    {
        gameRunner.UpdateWorld();

        var transition = gameRunner.ConsumeTransition();
        if (transition != null) DoTransition(transition.Value);
    }


    public void Transition(string request)
    {
        var transit = new TransitionReady(request);
        DoTransition(transit);
    }


    private void DoTransition(TransitionReady request)
    {
        gameRunner.Clear();
        gameBuilder.ClearContainer();

        var sceneDesc = gameBuilder.GetBuildForScene(request.To);

        foreach (var desc in sceneDesc.Descriptors)
        {
            var group = new SystemGroup(desc);
            gameBuilder.Build(group);
            gameRunner.AddGroup(group);
        }

        gameRunner.CollectEntitys();
        gameRunner.EnterSceneEvent();
    }
}
