using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{
    private bool isPaused => Time.timeScale == 0f;


    public void SetTime(float timeValue)
    {
        Time.timeScale = timeValue;
    }


    private void OnEscPressed(InputAction.CallbackContext _) => SetPause(!isPaused);
    public void SetPause(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;
        Input.Instance.SetInputMode(pause ? Input.Instance.GameInput.UI : Input.Instance.GameInput.Gameplay);
    }

    
    void Start()
    {
        Input.Instance.GameInput.System.Esc.started += OnEscPressed;

    }

    void OnDestroy()
    {
        Input.Instance.GameInput.System.Esc.started -= OnEscPressed;
    }
}
