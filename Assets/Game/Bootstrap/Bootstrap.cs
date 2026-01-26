using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public BaseProxy[] states;

    private StateMachine stateMachine = new();
    private EventWorld eventWorld = new();

    void Awake()
    {
        AddStates();
        stateMachine.SetMode(states[0]);
    }


    private void AddStates()
    {
        foreach (var state in states)
        {
            stateMachine.AddState(state);

            state.SetEventBus(eventWorld);
            state.Init();
        }
    }


    void OnDestroy()
    {
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
