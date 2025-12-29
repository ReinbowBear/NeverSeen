using System;
using UnityEngine.InputSystem;

public class Input : IDisposable
{
    private GameInput gameInput;
    private InputActionMap currentMap;

    public GameInput GameInput => gameInput;
    public GameInput.UIActions UI => gameInput.UI;
    public GameInput.GamePlayActions GamePlay => gameInput.GamePlay;
    public GameInput.SystemActions System => gameInput.System;
    public GameInput.DebugActions Debug => gameInput.Debug;


    public void Init()
    {
        gameInput = new GameInput();
        gameInput.System.Enable();
        gameInput.Debug.Enable();
    }


    public void SwitchTo(InputMode mode)
    {
        currentMap.Disable();

        currentMap = mode switch
        {
            InputMode.UI => gameInput.UI.Get(),
            InputMode.GamePlay => gameInput.GamePlay.Get(),
            InputMode.System => gameInput.System.Get(),
            InputMode.Debug => gameInput.Debug.Get(),

            _ => throw new ArgumentOutOfRangeException(nameof(mode))
        };

        currentMap.Enable();
    }

    public void SetActive(bool isActive)
    {
        if (isActive) currentMap.Enable();
        else currentMap.Disable();
    }


    public void Dispose()
    {
        gameInput.Dispose();
    }
}

public enum InputMode
{
    UI,
    GamePlay,
    System,
    Debug
}
