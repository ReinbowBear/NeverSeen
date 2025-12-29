using UnityEngine;
using UnityEngine.InputSystem;

public class UIFacade : MonoBehaviour
{
    private Input input;

    private PanelStack panelStack = new();
    private Panel CurrentPanel => panelStack.CurrentPanel;

    [SerializeField] private NavigateObject navigateObject;


    public void OpenPanel(Panel panel)
    {
        panelStack.OpenPanel(panel);
    }

    public void ClosePanel(InputAction.CallbackContext context = default)
    {
        panelStack.ClosePanel();
    }


    public void NavigateInput(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();

        int direction = (input == 1) ? -1 : 1;
        int newButtonIndex = (CurrentPanel.CurrentButtonIndex + direction + CurrentPanel.Buttons.Length) % CurrentPanel.Buttons.Length; // если вышли за край, возращаемся с другой стороны

        CurrentPanel.ChoseButtonByIndex(newButtonIndex);
        navigateObject.MoveTo(CurrentPanel.CurrentButton.transform);
    }

    public void InvokeButton(InputAction.CallbackContext context)
    {
        CurrentPanel.InvokeButton();
    }


    public void ActiveInput()
    {
        input.UI.Navigate.started += NavigateInput;
        input.UI.Submit.started += InvokeButton;
        input.System.Esc.started += ClosePanel; // не активируется потому что карта инпутов другая!
    }

    public void DisableInput()
    {
        input.UI.Navigate.started -= NavigateInput;
        input.UI.Submit.started -= InvokeButton;
        input.System.Esc.started -= ClosePanel;
    }
}
