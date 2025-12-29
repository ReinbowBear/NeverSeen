using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField, SerializeInterface(typeof(IState))] private IState StartedState;
    [SerializeField, SerializeInterface(typeof(IState))] private IState[] states;

    private StateMachine stateMachine = new();
    private ServicesContext servicesContext = new();


    void Awake()
    {
        AddStates();
        servicesContext.Input.Init();
        stateMachine.SetCurrent(StartedState); // немного костыльная функция
    }


    private void AddStates()
    {
        foreach (var state in states)
        {
            stateMachine.AddState(state);

            var gameState = state as IGameState;
            gameState.Init(servicesContext);
        }
    }


    void OnDestroy()
    {
        stateMachine.Exit();
        servicesContext.Factory.Dispose();
    }
}

public interface IGameState
{
    void Init(ServicesContext servicesContext);
}

public class ServicesContext
{
    public CommandBus commandBus = new();
    public EventBus eventBus = new();

    public Input Input = new();
    public AudioService AudioService = new();

    public SaveLoad SaveLoad = new();
    public Factory Factory = new();
}
