using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private bool isRunning;

    public async Task RunAsync(int sceneIndex, Task fadeOutTask, Task fadeInTask)
    {
        if (isRunning) return;
        isRunning = true;

        await fadeOutTask;

        var sceneLoad = SceneManager.LoadSceneAsync(sceneIndex);
        sceneLoad.allowSceneActivation = false;

        while (sceneLoad.progress < 0.9f) await Task.Yield();
        sceneLoad.allowSceneActivation = true;

        await fadeInTask;

        isRunning = false;
    }
}
