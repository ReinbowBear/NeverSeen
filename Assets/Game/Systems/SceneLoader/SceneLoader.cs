using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : ISystem
{
    private List<FadeScreen> activeFades = new();

    public void SetSubs(SystemSubs subs)
    {
        subs.AddListener(Run);
        subs.AddListener<FadeScreen>(OnSceneEnter).OnEvent<OnSceneEnter>();
        subs.AddListener<FadeScreen>(OnSceneClose).OnEvent<OnSceneClose>();
    }


    public void OnSceneEnter(FadeScreen fadeScreen)
    {
        StartFade(fadeScreen, 1, 0);
    }

    public void OnSceneClose(FadeScreen fadeScreen)
    {
        StartFade(fadeScreen, 0, 1);
    }


    private void StartFade(FadeScreen screen, float from, float to)
    {
        screen.IsRunning = true;
        screen.Time = 0f;
        screen.From = from;
        screen.To = to;

        screen.canvasGroup.alpha = from;

        if (!activeFades.Contains(screen)) 
        {
            activeFades.Add(screen);
        }
    }

    public void Run()
    {
        for (int i = activeFades.Count - 1; i >= 0; i--)
        {
            var screen = activeFades[i];
            Fade(screen);

            if (!screen.IsRunning)
            {
                activeFades.RemoveAt(i);
            }
        }
    }



    private void FadeScene(FadeScreen fadeScreen, float from, float to)
    {
        if (!fadeScreen.IsRunning)
        {
            fadeScreen.IsRunning = true;
            fadeScreen.canvasGroup.alpha = 0;
            fadeScreen.From = from;
            fadeScreen.To = to;
        }

        if (fadeScreen.From >= fadeScreen.To)
        {
            fadeScreen.IsRunning = false;
        }
        else
        {
            Fade(fadeScreen);
        }
    }


    private void Fade(FadeScreen screen)
    {
        screen.Time += Time.deltaTime;

        var time = screen.Time / screen.fadeDuration;
        time = Mathf.Clamp01(time);

        screen.canvasGroup.alpha = Mathf.Lerp(screen.From, screen.To, time);

        if (time >= 1f)
        {
            screen.IsRunning = false;
        }
    }


    public async Task RunAsync(int sceneIndex)
    {
        var sceneLoad = SceneManager.LoadSceneAsync(sceneIndex);
        sceneLoad.allowSceneActivation = false;

        while (sceneLoad.progress < 0.9f) await Task.Yield();
        sceneLoad.allowSceneActivation = true;
    }
}
