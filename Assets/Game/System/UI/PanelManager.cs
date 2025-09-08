using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private Panel rootPanel;

    private Stack<Panel> PanelsStack = new();
    private Panel currentPanel => PanelsStack.Count > 0 ? PanelsStack.Peek() : null;
    private Panel activePanel => currentPanel ?? rootPanel;

    private Input input;

    [Inject]
    public void Construct(Input input)
    {
        this.input = input;
    }

    void Start()
    {
        input.UI.Navigate.started += NavigateInput;
        input.UI.Submit.started += InvokeButton;
        input.System.Esc.started += ClosePanel;

        input.SetInputByIndex(0);

        if (rootPanel == null) return;
        rootPanel.FocusFirstButton(); // выделяем первую кнопку менюшки (со звуком соотведственно получается)
    }


    private void NavigateInput(InputAction.CallbackContext context)
    {
        if (activePanel == null) return;
        if (activePanel.Buttons.Length == 0) { Debug.Log("застряли"); return; }

        float input = context.ReadValue<float>();

        int direction = (input == 1) ? -1 : 1; // смотрим направление ввода
        int newButtonIndex = (activePanel.CurrentButtonIndex + direction + activePanel.Buttons.Length) % activePanel.Buttons.Length; // если вышли за край, возращаемся с другой стороны

        activePanel.ChoseButtonByIndex(newButtonIndex);
    }

    private void InvokeButton(InputAction.CallbackContext _)
    {
        (activePanel?.CurrentButton)?.onClick.Invoke();
    }

    private void ClosePanel(InputAction.CallbackContext _)
    {
        if (currentPanel == null) return;

        currentPanel.SetActive(false);
        PanelsStack.Pop();
    }


    public void OpenPanel(Panel panel)
    {
        PanelsStack.Push(panel);
        panel.SetActive(true);
    }


    void OnEnable()
    {
        //input.UI.Navigate.started += NavigateInput;
        //input.UI.Submit.started += InvokeButton;
        //input.System.Esc.started += ClosePanel;
    }

    void OnDisable()
    {
        input.UI.Navigate.started -= NavigateInput;
        input.UI.Submit.started -= InvokeButton;
        input.System.Esc.started -= ClosePanel;
    }
}
