using UnityEngine.InputSystem;
using Zenject;

public class Input : IInitializable
{
    public GameInput GameInput { get; private set; }

    public GameInput.UIActions UI { get; private set; }
    public GameInput.GamePlayActions GamePlay { get; private set; }
    public GameInput.SystemActions System { get; private set; }
#if UNITY_EDITOR
    public GameInput.DebugActions Debug { get; private set; }
#endif
    private InputActionMap currentInputs;

    public void Initialize()
    {
        GameInput = new GameInput();

        GameInput.System.Enable();

#if UNITY_EDITOR
        GameInput.Debug.Enable();
#endif

        UI = GameInput.UI;
        GamePlay = GameInput.GamePlay;
        System = GameInput.System;
        Debug = GameInput.Debug;
    }


    public void SetInputByIndex(byte index)
    {
        switch (index)
        {
            case 0:
                SetInputMode(UI);
                break;

            case 1:
                SetInputMode(GamePlay);
                break;

            case 2:
                SetInputMode(System);
                break;

            case 3:
                SetInputMode(Debug);
                break;
        }
    }

    public void SetInputMode(InputActionMap inputMap)
    {
        if (currentInputs != null)
        {
            currentInputs.Disable();
        }

        inputMap.Enable();
        currentInputs = inputMap;
    }

    public void SetActiveInputs(bool varBool)
    {
        if (varBool)
        {
            currentInputs.Enable();
        }
        else currentInputs.Disable();
    }
}
