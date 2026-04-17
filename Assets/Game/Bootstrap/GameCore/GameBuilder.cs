
public class GameBuilder
{
    private SceneConfigs sceneConfigs = new();
    private Container container = new();


    public CompositeSceneDesc GetBuildForScene(string sceneName)
    {
        var descs = sceneConfigs.GetConfigFor(sceneName);
        return descs;
    }

    public void Build(SystemGroup systemGroup)
    {
        foreach (var phase in systemGroup.Phases)
        {
            var subs = phase.SystemSubs;
            var types = phase.SystemTypes;

            for (int i = 0; i < types.Count; i++)
            {
                var type = types[i];

                var system = (ISystem)container.Resolve(type);
                var SystemSubs = new SystemSubs();

                system.SetSubs(SystemSubs);
                subs.Add(SystemSubs);
            }
        }
    }


    public void ClearContainer()
    {
        container.Dispose();
        container.Clear();
    }
}
