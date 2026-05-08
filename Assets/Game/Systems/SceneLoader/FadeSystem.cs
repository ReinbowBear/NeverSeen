using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSystem : ISystem
{
    public void Execute(World world, EntityCommands commands)
    {
        HandleStart(world, commands);
        HandleFade(world, commands);
    }


    private void HandleStart(World world, EntityCommands commands)
    {
        foreach (var (fade, entity) in world.Query<Fade>())
        {
            if (world.Has<OnSceneEnter>())
            {
                StartFade(fade, 1f, 0f);
                commands.AddComponent(entity, new RunTag());
            }
            else if (world.Has<OnSceneClose>())
            {
                StartFade(fade, 0f, 1f);
                commands.AddComponent(entity, new RunTag());
            }
        }
    }

    private void HandleFade(World world, EntityCommands commands)
    {
        foreach (var (fade, tag, entity) in world.Query<Fade, RunTag>().Require<RunTag>())
        {
            Fade(fade);

            if (fade.Time >= fade.fadeDuration)
            {
                commands.RemoveComponent<RunTag>(entity);
            }
        }
    }


    private void StartFade(Fade fade, float from, float to)
    {
        fade.Time = 0f;
        fade.From = from;
        fade.To = to;

        fade.canvasGroup.alpha = from;
    }

    private void Fade(Fade screen)
    {
        screen.Time += Time.deltaTime;

        var time = screen.Time / screen.fadeDuration;
        time = Mathf.Clamp01(time);

        screen.canvasGroup.alpha = Mathf.Lerp(screen.From, screen.To, time);
    }


    public async Task RunAsync(int sceneIndex)
    {
        var sceneLoad = SceneManager.LoadSceneAsync(sceneIndex);
        sceneLoad.allowSceneActivation = false;

        while (sceneLoad.progress < 0.9f) await Task.Yield();
        sceneLoad.allowSceneActivation = true;
    }
}
