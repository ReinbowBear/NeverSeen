using System.Collections.Generic;

public class GroupBuilder
{
    private SceneConfigs sceneConfigs = new();
    private Container container = new();


    public CompositeSceneDesc GetGroupsForScene(string sceneName)
    {
        var descs = sceneConfigs.GetConfigFor(sceneName);
        return descs;
    }


    public SystemGroup BuildSystems(SystemGroupDesc groupDesc)
    {
        var runtimeGroup = new SystemGroup();

        for (int i = 0; i < groupDesc.Phases.Length; i++)
        {
            var types = groupDesc.Phases[i].SystemTypes;
            var runtimePhase = new List<ISystem>();

            foreach (var type in types)
            {
                var system = (ISystem)container.Resolve(type);
                runtimePhase.Add(system);
            }

            runtimeGroup.Phases[i] = runtimePhase;
        }

        return runtimeGroup;
    }


    public void ClearContainer()
    {
        container.Dispose();
        container.Clear();
    }
}
