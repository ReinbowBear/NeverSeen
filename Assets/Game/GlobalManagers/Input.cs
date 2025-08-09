using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    public static Input Instance;

    public GameInput GameInput;
    private InputActionMap currentInputs;

    public event Action<string> OnRebindComplete;
    public event Action<string> OnRebindCanceled;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    private void Awake()
    {
        Instance = this;
        GameInput = new GameInput();

        GameInput.System.Enable();

        #if UNITY_EDITOR
        GameInput.Debug.Enable();
        #endif
    }


    public void SetInputMode(InputActionMap newInputsMap) // например Input.Instance.GameInput.Menu
    {
        if (currentInputs != null)
        {
            currentInputs.Disable();
        }

        newInputsMap.Enable();
        currentInputs = newInputsMap;
    }

    public void ActiveInputs(bool varBool)
    {
        if (varBool)
        {
            currentInputs.Enable();
        }
        else currentInputs.Disable();
    }

    #region Rebindg
    public void RebindInputs(InputAction actionToRebind, int bindingIndex = 0)
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

        StartCoroutine(DoRebind(actionToRebind, bindingIndex));
    }

    private IEnumerator DoRebind(InputAction action, int bindingIndex)
    {
        yield return new WaitForEndOfFrame(); // отключение кнопки и задержка для корректной работы
        action.Disable();

        rebindOperation = action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("Mouse") // исключаем мышь
            .WithCancelingThrough("<Keyboard>/escape") // отмена переназначения через escape

            .OnComplete(RebindComplete)
            .OnCancel(RebindCanceled);

        rebindOperation.Start();
    }

    private void RebindComplete(InputActionRebindingExtensions.RebindingOperation operation)
    {
        rebindOperation.Dispose();
        operation.action.Enable();
        OnRebindComplete?.Invoke(operation.action.name);
    }

    private void RebindCanceled(InputActionRebindingExtensions.RebindingOperation operation)
    {
        rebindOperation.Dispose();
        operation.action.Enable();
        OnRebindCanceled?.Invoke(operation.action.name);
    }


    public void CancelRebinding()
    {
        rebindOperation?.Cancel();
    }


    public void SaveBindings()
    {
        string json = GameInput.asset.SaveBindingOverridesAsJson();
        File.WriteAllText(MyPaths.INPUTS + "/SaveInputs.json", json);
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
    #endregion
}
