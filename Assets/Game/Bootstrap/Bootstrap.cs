using System;
using UnityEngine;

public static class GameEntry
{
    private static SystemActivator activator;
    private static GameRunner gameRunner;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void HelloWorld()
    {
        activator = new();
        gameRunner = new();

        activator.CacheTypes();
        activator.SortTypes();

        MapSystems();
        CreateUpdate();
    }


    private static void MapSystems()
    {
        foreach (var stateObj in Enum.GetValues(typeof(UpdateState)))
        {
            var state = (UpdateState)stateObj;
            var enumerable = activator.GetSystems(state);
            var context = new SceneContext(enumerable);
    
            gameRunner.AddContext(state, context);
        }
    }

    private static void CreateUpdate()
    {
        var obj = new GameObject($"{nameof(UpdateRunner)}")
        {
            hideFlags = HideFlags.HideAndDontSave
        };

        var runner = obj.AddComponent<UpdateRunner>();
        runner.Init(gameRunner);

        GameObject.DontDestroyOnLoad(obj);
    }


    public static void ExitGame()
    {
        #if UNITY_EDITOR
        Debug.Log("Отсюда нет выхода.. x_x");
        #endif

        Application.Quit();
    }
}

public class UpdateRunner : MonoBehaviour
{
    private GameRunner gameRunner;

    public void Init(GameRunner gameRunner)
    {
        this.gameRunner = gameRunner;
    }


    void Update()
    {
        gameRunner.UpdateWorld();
    }
}
