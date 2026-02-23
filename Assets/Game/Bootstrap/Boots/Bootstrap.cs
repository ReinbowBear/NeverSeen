using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private EventWorld eventWorld = new();
    private DependencyResolver resolver;
    private Container container;

    void Awake()
    {
        container = new();
        resolver = new(container);

        container.Add(eventWorld);
    }

    void Start()
    {
        InitInstallers();
        ResolveSystems();

        InitSystems();
        InitEventListeners();

        eventWorld.Invoke(Events.SceneEvents.EnterScene);
    }


    private void InitInstallers()
    {
        foreach (var intaller in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IInstaller>())
        {
            intaller.Install(container);
        }
    }

    private void ResolveSystems()
    {
        foreach (var proxy in container.EventListeners)
        {
            resolver.Resolve(proxy);
        }
    }


    private void InitSystems()
    {
        foreach (var initializer in container.Initializables)
        {
            initializer.Init();
        }
    }

    private void InitEventListeners()
    {
        foreach (var listener in container.EventListeners)
        {
            listener.SetEvents(eventWorld);
        }
    }


    private void DisposeSystems()
    {
        foreach (var disposable in container.Disposables)
        {
            disposable.Dispose();
        }
    }

    void OnDestroy()
    {
        eventWorld.Invoke(Events.SceneEvents.CloseScene);
        DisposeSystems();
    }


    public void ExitGame()
    {
        #if UNITY_EDITOR
        Debug.Log("Отсюда нет выхода.. x_x");
        #endif

        Application.Quit();
    }
}
