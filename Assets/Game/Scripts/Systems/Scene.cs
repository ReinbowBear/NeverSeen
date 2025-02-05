using UnityEngine.SceneManagement;

public static class Scene
{
    private static byte currentScene;

    static Scene()
    {
        EventBus.Add<MyEvent.OnSave>(Save);
    }


    public static void Continue()
    {
        currentScene = SaveSystem.gameData.generalData.sceneIndex;
        SceneManager.LoadScene(currentScene);
    }

    public static void Load(byte sceneIndex)
    {
        currentScene = sceneIndex;
        SceneManager.LoadScene(sceneIndex);
    }


    private static void Save(MyEvent.OnSave _)
    {
        SaveSystem.gameData.generalData.sceneIndex = currentScene;
    }
}
