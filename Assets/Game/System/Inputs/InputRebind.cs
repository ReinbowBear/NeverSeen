using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputRebind : MonoBehaviour
{
    private string filePath = Path.Combine(MyPaths.INPUTS, "SaveInputs.json");
    private string filePathDefault = Path.Combine(MyPaths.INPUTS, "DefaultInputs.json");

    private Input input;

    public event Action<string> OnRebindComplete;
    public event Action<string> OnRebindCanceled;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    [Inject]
    public void Construct(Input input)
    {
        this.input = input;
    }


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
        string json = input.GameInput.asset.SaveBindingOverridesAsJson();

        Directory.CreateDirectory(MyPaths.INPUTS);
        File.WriteAllText(filePath, json);
    }

    public void ResetToDefaultBindings()
    {
        if (File.Exists(filePathDefault) == false)
        {
            Debug.LogError("Файл с дефолтными биндами не найден по пути: " + filePathDefault);
            return;
        }

        string json = File.ReadAllText(filePathDefault);
        input.GameInput.asset.LoadBindingOverridesFromJson(json);
    }
}
