using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputRebind2 : MonoBehaviour
{
    private Input input;

    public event Action<string> OnRebindComplete;
    public event Action<string> OnRebindCanceled;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

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


    public void SaveBindings(string fileName)
    {
        var json = input.GameInput.asset.SaveBindingOverridesAsJson();
        SaveLoad.Save(json, fileName, ConfigType.Input);
    }

    public void ResetToDefaultBindings(string fileName)
    {
        var json = SaveLoad.Load<string>(fileName, ConfigType.Input);
        if (string.IsNullOrEmpty(json)) return;

        input.GameInput.asset.LoadBindingOverridesFromJson(json);
    }
}
