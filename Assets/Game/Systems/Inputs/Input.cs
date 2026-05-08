using UnityEngine;

public class Input : ISystem
{
    public GameInput GameInput { get; private set; }
    private StateMachine<InputMode, IState> stateMachine = new();
    private ISystem CurrentState => (ISystem)stateMachine.CurrentState;

    public Input()
    {
        GameInput = new GameInput();
        GameInput.Enable();

        stateMachine.AddState(InputMode.None, new EmptySystem());
        stateMachine.AddState(InputMode.UI, new UIInputState(GameInput.UI));
        stateMachine.AddState(InputMode.GamePlay, new GamePlayInputState(GameInput.GamePlay));

        stateMachine.SetState(InputMode.None); // сначала происходит вызов систем а уже потом появляется в ворлд событие на смену инпута
    }


    public void Execute(World world, EntityCommands commands)
    {
        UpdateInputs(world, commands);
        CheckSwitchInputs(world);
    }


    private void UpdateInputs(World world, EntityCommands commands)
    {
        CurrentState.Execute(world, commands);
    }

    private void CheckSwitchInputs(World world)
    {
        foreach (var (switchInput, entity) in world.Query<SwitchInputInent>())
        {
            Debug.Log(switchInput.SwitchTo);
            SwitchTo(switchInput.SwitchTo);
        }
    }


    public void SwitchTo(InputMode mode)
    {
        stateMachine.SetState(mode);
    }

    public void SetActive(bool isActive)
    {
        stateMachine.SetActive(isActive);
    }
}

public struct SwitchInputInent
{
    public InputMode SwitchTo;

    public SwitchInputInent(InputMode mode)
    {
        SwitchTo = mode;
    }
}

public enum InputMode
{
    None,

    UI,
    GamePlay,
}

public struct EmptySystem : ISystem, IState
{
    public void Enter()
    {

    }

    public void Execute(World world, EntityCommands commands)
    {

    }

    public void Exit()
    {

    }
}
