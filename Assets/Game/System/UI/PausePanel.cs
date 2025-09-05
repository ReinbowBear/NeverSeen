using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Panel panel;
    private bool isActive;

    private Input input;
    private TimeManager timeManager;

    [Inject]
    public void Construct(Input input, TimeManager timeManager)
    {
        this.input = input;
        this.timeManager = timeManager;
    }

    private void OpenPausePanel(InputAction.CallbackContext _)
    {
        isActive = !isActive;

        timeManager.SetPause(isActive);
        panel.SetActive(isActive);
    }


    void OnEnable()
    {
        input.System.Esc.started += OpenPausePanel;
    }

    void OnDisable()
    {
        input.System.Esc.started -= OpenPausePanel;
    }
}
