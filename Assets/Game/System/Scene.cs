using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public static void Continue()
    {
        SceneManager.LoadScene(SaveSystem.gameData.generalData.sceneIndex);
    }

    public static void Load(byte index)
    {
        SaveSystem.gameData.generalData.sceneIndex = index;
        SceneManager.LoadScene(index);
    }


    void OnDestroy()
    {
        Loader.ReleaseAllAssets();
        Debug.Log("Все ассеты выгружены");
    }
}
