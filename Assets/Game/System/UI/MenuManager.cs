using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public GameInput menuInput;

    [SerializeField] private Panel basePanel;
    [SerializeField] private Panel pausePanel;
    [HideInInspector] public List<Panel> panels = new List<Panel>();
    private Panel currentPanel;

    void Awake()
    {
        instance = this;
        menuInput = new GameInput();

        CheckPanel();
    }


    private void NavigateInput(InputAction.CallbackContext context)
    {
        float input = menuInput.UI.Navigate.ReadValue<float>();

        int direction = (input == 1) ? -1 : 1; // смотрим направление ввода
        int newButtonIndex = (currentPanel.currentButton + direction + currentPanel.buttons.Count) % currentPanel.buttons.Count; // если вышли за край, возращаемся с другой стороны

        ChoseNewButton(newButtonIndex);
    }

    public void ChoseNewButton(int index)
    {
        currentPanel.ChoseNewButton(index);
    }


    private void InvokeButton(InputAction.CallbackContext context)
    {
        int index = currentPanel.currentButton;
        currentPanel.buttons[index].button.onClick.Invoke();
    }


    public void CheckPanel()
    {
        if (panels.Count != 0)
        {
            currentPanel = panels[panels.Count-1];
        }
        else if (basePanel != null)
        {
            currentPanel = basePanel;
        }
        else
        {
            currentPanel = null;
        }
    }

    private void ExitPanel(InputAction.CallbackContext context)
    {
        if (panels.Count != 0)
        {
            panels[panels.Count-1].ClosePanel(); // вызывает так же CheckPanel
        }
        else if (pausePanel != null)
        {
            pausePanel.OpenPanel();
        }
    }


    void OnEnable()
    {
        menuInput.Enable();

        menuInput.UI.Submit.started += InvokeButton;
        menuInput.UI.Navigate.started += NavigateInput;
        menuInput.UI.Esc.started += ExitPanel;
    }

    void OnDisable()
    {
        menuInput.UI.Submit.started -= InvokeButton;
        menuInput.UI.Navigate.started -= NavigateInput;
        menuInput.UI.Esc.started -= ExitPanel;

        menuInput.Disable();
    }
}
