using System;

public class Input : IDisposable
{
    public GameInput GameInput { get; private set; }
    private StateMachine stateMachine = new();

    public void Init(EventWorld eventWorld)
    {
        GameInput = new GameInput();
        GameInput.Enable();
        
        var gamePlayState = new GamePlayInputState(GameInput, eventWorld);
        var UIstate = new UIInputState(GameInput, eventWorld);

        stateMachine.AddState(gamePlayState);
        stateMachine.AddState(UIstate);
    }


    public void SwitchTo(InputMode mode)
    {
        switch (mode)
        {
            case InputMode.UI:
                stateMachine.SetMode<UIInputState>();
                break;

            case InputMode.GamePlay:
                stateMachine.SetMode<GamePlayInputState>();
                break;
        }
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
