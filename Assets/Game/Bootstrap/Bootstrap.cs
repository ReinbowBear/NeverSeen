using UnityEngine;
using UnityEngine.SceneManagement;

public static class Bootstrap
{
    private static Game game = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnScene()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        game.Transition(sceneName);

        CreateUpdate();
    }


    private static void CreateUpdate()
    {
        var obj = new GameObject($"{nameof(UpdateRunner)}")
        {
            hideFlags = HideFlags.HideAndDontSave
        };

        var runner = obj.AddComponent<UpdateRunner>();
        runner.Init(game);

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
    private Game game;

    public void Init(Game game)
    {
        this.game = game;
    }


    void Update()
    {
        game.Update();
    }
}
