using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : IDisposable
{
    private bool isPaused => Time.timeScale == 0f;
    private Input input;

    public TimeManager(Input newInput)
    {
        input = newInput;
        input.GameInput.System.Esc.started += OnEscPressed;
    }

    public void SetTime(float timeValue)
    {
        Time.timeScale = timeValue;
    }


    private void OnEscPressed(InputAction.CallbackContext _) => SetPause(!isPaused);
    public void SetPause(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
        input.SetInputMode(paused ? input.GameInput.UI : input.GameInput.Gameplay);
    }


    public void Dispose()
    {
        input.GameInput.System.Esc.started -= OnEscPressed;
        Debug.Log("Dispose сработал");
    }
}
