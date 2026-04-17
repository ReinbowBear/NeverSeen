using UnityEngine;

public class Input : ISystem
{
    public GameInput GameInput { get; private set; }
    private StateMachine<InputMode, IState> stateMachine = new();
    private IUpdate CurrentState => (IUpdate)stateMachine.CurrentState;

    public Input(UIInput uiInput, GamePlayInput gamePlayInput)
    {
        GameInput = new GameInput();
        GameInput.Enable();

        stateMachine.AddState(InputMode.UI, new UIInputState(GameInput.UI, uiInput));
        stateMachine.AddState(InputMode.GamePlay, new GamePlayInputState(GameInput.GamePlay, gamePlayInput));

        SwitchTo(InputMode.UI); // по идеи должны устанавливать инпут чреез какое нибудь событие с аргументом внутри...
        Debug.Log("костыльный выбор инпутов");
    }

    public void SetSubs(SystemSubs subs)
    {
        //subs.AddListener<World>(UpdateSystem);
    }


    public void UpdateSystem(World world)
    {
        CurrentState.Update(world);
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

public enum InputMode
{
    UI,
    GamePlay,
}

public interface IUpdate
{
    void Update(World world);
}
