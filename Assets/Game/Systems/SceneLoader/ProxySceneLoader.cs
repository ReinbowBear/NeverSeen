using System.Threading.Tasks;
using UnityEngine;

public class ProxySceneLoader : BaseProxy
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;

    private SceneLoader sceneLoader = new();

    public override void Init()
    {
        _ = Fade(1f, 0f);
    }


    public async void LoadScene(int sceneIndex)
    {
        await sceneLoader.RunAsync(sceneIndex, Fade(0f, 1f), Fade(1f, 0f));
    }

    private async Task Fade(float from, float to)
    {
        float time = 0f;
        canvasGroup.alpha = from;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, time / fadeDuration);
            await Task.Yield();
        }

        canvasGroup.alpha = to;
    }
}
