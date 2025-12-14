using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private byte sceneIndex;
    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration;

    private Input input;
    private Coroutine coroutine;

    [Inject]
    public void Construct(Input input)
    {
        this.input = input;
    }

    private void StartScene(OnSceneStart _)
    {
        StartCoroutine(Fade(1f, 0f));
        input.SwitchTo((InputMode)sceneIndex);
    }


    public void LoadSceneWithFade(string sceneName)
    {
        if (coroutine != null) return;

        coroutine = StartCoroutine(FadeAndLoadScene(sceneName));
        input.SetActive(false);
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        yield return StartCoroutine(Fade(0f, 1f));

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    private IEnumerator Fade(float from, float to)
    {
        canvasGroup.alpha = from;

        float time = 0f;
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(from, to, time / fadeDuration);
            canvasGroup.alpha = alpha;

            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }
}
