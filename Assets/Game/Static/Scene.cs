using UnityEngine.SceneManagement;

public static class Scene
{
    private static byte currentScene;

    static Scene()
    {
        SaveSystem.onSave += Save;
    }


    public static void Continue()
    {
        currentScene = SaveSystem.gameData.saveScene.currentScene;
        SceneManager.LoadScene(currentScene);
    }

    public static void Load(byte sceneIndex)
    {
        currentScene = sceneIndex;
        SceneManager.LoadScene(sceneIndex);
    }


    private static void Save()
    {
        SaveScene saveScene = new SaveScene();
        saveScene.currentScene = currentScene;

        SaveSystem.gameData.saveScene = saveScene;
    }
}

[System.Serializable]
public struct SaveScene
{
    public byte currentScene;
}
