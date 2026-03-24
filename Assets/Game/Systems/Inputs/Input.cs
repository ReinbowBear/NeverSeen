using System;

public class Input : IDisposable
{
    public GameInput GameInput { get; private set; }
    private StateMachine<InputMode, IState> stateMachine = new();

    public void Init(EventWorld eventWorld)
    {
        GameInput = new GameInput();
        GameInput.Enable();
        
        var gamePlayState = new GamePlayInputState(GameInput, eventWorld);
        var UIstate = new UIInputState(GameInput, eventWorld);

        stateMachine.AddState(InputMode.GamePlay, gamePlayState);
        stateMachine.AddState(InputMode.UI, UIstate);
    }


    public void SwitchTo(InputMode mode)
    {
        stateMachine.SetState(mode);
    }


    public void Dispose()
    {
        stateMachine.Dispose();
        GameInput.Disable(); // не уверен есть ли смысл дисейблить перед дисповсом
        GameInput.Dispose();
    }
}

public enum InputMode
{
    UI,
    GamePlay,
}
