using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    [SerializeField] private string[] assetsToLoad;

    public static void Continue()
    {
        SceneManager.LoadScene(SaveLoad.gameData.sceneIndex);
    }

    public static void Load(byte index)
    {
        SaveLoad.gameData.sceneIndex = index;
        SceneManager.LoadScene(index);
    }


    void OnDestroy()
    {
        Loader.ReleaseAllAssets();
        CoroutineManager.CLear();
        Debug.Log("Все ассеты выгружены");
    }


    private async void LoadSceneAndAssets()
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(0);
        sceneLoad.allowSceneActivation = false;


        foreach (var key in assetsToLoad)
        {
            var handle = Loader.LoadAssetAsync<GameObject>(key);
            await handle;
        }

        while (sceneLoad.progress < 0.9f)
        {
            await Task.Yield();
        }

        sceneLoad.allowSceneActivation = true;
    }
}
