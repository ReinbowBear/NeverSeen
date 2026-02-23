using System.Threading.Tasks;
using UnityEngine;

public class ProxySceneLoader : MonoBehaviour, IEventListener
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;

    private SceneLoader sceneLoader = new();

    public void SetEvents(EventWorld eventWorld)
    {
        eventWorld.AddListener(this, FadeOut, Events.SceneEvents.EnterScene);
    }


    public async void LoadScene(int sceneIndex)
    {
        await sceneLoader.RunAsync(sceneIndex, Fade(0f, 1f), Fade(1f, 0f));
    }

    private void FadeOut()
    {
        _ = Fade(1f, 0f);
    }

    private void FadeTo()
    {
        _ = Fade(0f, 1f);
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
