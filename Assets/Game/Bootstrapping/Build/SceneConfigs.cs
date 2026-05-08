using System;
using System.Collections.Generic;

public class SceneConfigs
{
    private Dictionary<string, Func<CompositeSceneDesc>> configs = new();

    public SceneConfigs()
    {
        configs["Menu"] = ConfigForMenu;
        // configs["Meta"] = ConfigForMenu;
        configs["GamePlay"] = ConfigForGamePlay;
    }


    public CompositeSceneDesc GetConfigFor(string sceneName)
    {
        if (!configs.TryGetValue(sceneName, out var config)) throw new ArgumentException($"Unknown scene: {sceneName}");
        return config();
    }


    private CompositeSceneDesc ConfigForMenu()
    {
        return new CompositeSceneDesc
        (
            new CoreModule(),
            new MenuModule()
        );
    }

    private CompositeSceneDesc ConfigForGamePlay()
    {
        return new CompositeSceneDesc
        (
            new CoreModule(),
            new GamePlayModule()
        );
    }
}

public enum SystemGroupEnum // пока на строках работаем но может переделаем
{
    Global,

    Menu,
    Gameplay,
    Pause,
}
