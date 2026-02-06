using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public ProxyState[] states;

    private StateMachine stateMachine = new();
    private EventWorld eventWorld = new();
    private DependencyResolver resolver = new();

    void Awake()
    {
        AddStates();
        stateMachine.SetMode(states[0]);

        eventWorld.Invoke(Events.SceneEvents.EnterScene);
    }


    private void AddStates()
    {
        foreach (var state in states)
        {
            stateMachine.AddState(state);
            state.Init(eventWorld, resolver);
        }
    }


    void OnDestroy()
    {
        eventWorld.Invoke(Events.SceneEvents.CloseScene);
        stateMachine.Dispose();
    }


    public void ExitGame()
    {
        #if UNITY_EDITOR
        Debug.Log("Отсюда нет выхода.. x_x");
        #endif

        Application.Quit();
    }
}
