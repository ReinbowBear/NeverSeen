using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public static void Continue()
    {
        SceneManager.LoadScene(GameData.sceneIndex);
    }

    public static void Load(byte index)
    {
        GameData.sceneIndex = index;
        SceneManager.LoadScene(index);
    }


    void OnDestroy()
    {
        ObjectPool.Clear();
        CoroutineManager.Clear();
        Debug.Log("Все ассеты выгружены");
    }
}
