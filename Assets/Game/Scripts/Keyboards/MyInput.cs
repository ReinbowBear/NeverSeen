using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyInput : MonoBehaviour
{
    public static MyInput Instance;
    public GameInput GameInput;
    public GameInput.MenuActions Menu;
    public GameInput.PlayerActions Player;

    public event Action<string> OnRebindComplete;
    public event Action<string> OnRebindCanceled;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    private void Awake()
    {
        Instance = this;

        GameInput = new GameInput();
        Menu = GameInput.Menu;
        Player = GameInput.Player;
    }

    public void SwitchToMenu()
    {
        GameInput.Menu.Enable();
        GameInput.Player.Disable();
    }

    public void SwitchToPlayer()
    {
        GameInput.Menu.Disable();
        GameInput.Player.Enable();
    }


    public void StartRebinding(InputAction actionToRebind, int bindingIndex = 0)
    {
        if (actionToRebind == null)
        {
            Debug.LogError("Action is null");
            return;
        }

        if (bindingIndex < 0 || bindingIndex >= actionToRebind.bindings.Count)
        {
            Debug.LogError("Invalid binding index");
            return;
        }

        StartCoroutine(RebindInput(actionToRebind, bindingIndex));
    }

    private IEnumerator RebindInput(InputAction action, int bindingIndex)
    {
        yield return null; // отключение кнопки и задержка для корректной работы
        action.Disable();

        rebindOperation = action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("Mouse") // исключаем мышь
            .WithCancelingThrough("<Keyboard>/escape") // отмена переназначения через escape
            .OnComplete(operation =>
            {
                action.Enable();
                rebindOperation.Dispose();
                OnRebindComplete?.Invoke(action.name);
            })
            .OnCancel(operation =>
            {
                action.Enable();
                rebindOperation.Dispose();
                OnRebindCanceled?.Invoke(action.name);
            });

        rebindOperation.Start();
    }

    public void CancelRebinding()
    {
        rebindOperation?.Cancel();
    }


    public void ResetToDefaultBindings()
    {
        string filePath = MyPaths.INPUTS + "/DefaultInputs.json";

        if (File.Exists(filePath) == false)
        {
            Debug.LogError("Файл с дефолтными биндами не найден по пути: " + filePath);
            return;
        }

        string json = File.ReadAllText(filePath);
        GameInput.asset.LoadBindingOverridesFromJson(json);
    }


    void OnEnable()
    {
        GameInput.Enable();
    }

    void OnDisable()
    {
        GameInput.Disable();
    }
}