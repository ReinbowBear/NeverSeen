using UnityEngine.InputSystem;
using Zenject;

public class Input : IInitializable
{
    public static Input Instance;
    private GameState gameState;

    public GameInput GameInput { get; private set; }
    public InputActionMap UI { get; private set; }
    public InputActionMap GamePlay { get; private set; }
    public InputActionMap System { get; private set; }
    #if UNITY_EDITOR
    public InputActionMap Debug { get; private set; }
    #endif
    private InputActionMap currentInputs;

    public Input(GameState gameState)
    {
        this.gameState = gameState;
    }

    public void Initialize()
    {
        Instance = this;
        GameInput = new GameInput();

        GameInput.System.Enable();

        #if UNITY_EDITOR
        GameInput.Debug.Enable();
        #endif

        UI = GameInput.UI;
        GamePlay = GameInput.Gameplay;

        #if UNITY_EDITOR
        #endif

        SetInputByIndex(gameState.sceneIndex);
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
