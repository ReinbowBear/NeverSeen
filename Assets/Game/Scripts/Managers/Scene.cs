using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    private static byte sceneIndex;

    public static void Continue()
    {
        sceneIndex = SaveSystem.gameData.generalData.sceneIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public static void Load(byte index)
    {
        sceneIndex = index;
        SceneManager.LoadScene(index);
    }

    private void Save(MyEvent.OnSave _)
    {
        SaveSystem.gameData.generalData.sceneIndex = sceneIndex;
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnSave>(Save);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnSave>(Save);
    }
}
