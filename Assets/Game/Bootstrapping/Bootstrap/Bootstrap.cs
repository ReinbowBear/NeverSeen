using UnityEngine;
using UnityEngine.SceneManagement;

public static class Bootstrap
{
    private static Game game;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void BeforeScene()
    {
        game = new();
        CreateUpdate();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnScene()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        game.DoTransition(sceneName);
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




//public class Bootstrap : MonoBehaviour
//{
//    private static Bootstrap instance;
//    private Game game;
//
//    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
//    private static void Init()
//    {
//        if (instance != null) return;
//
//        var obj = new GameObject(nameof(Bootstrap));
//        instance = obj.AddComponent<Bootstrap>();
//
//        DontDestroyOnLoad(obj);
//    }
//
//    private void Awake()
//    {
//        if (instance != null && instance != this)
//        {
//            Destroy(gameObject);
//            return;
//        }
//
//        instance = this;
//        game = new Game();
//        SceneManager.sceneLoaded += OnSceneLoaded;
//    }
//
//
//    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//    {
//        game.DoTransition(scene.name);
//    }
//
//
//    private void Update()
//    {
//        game.Update();
//    }
//
//
//    void OnEnable()
//    {
//        //SceneManager.sceneLoaded += OnSceneLoaded;
//    }
//
//    void OnDestroy()
//    {
//        SceneManager.sceneLoaded -= OnSceneLoaded;
//    }
//}