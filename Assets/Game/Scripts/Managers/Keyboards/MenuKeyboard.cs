using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuKeyboard : MonoBehaviour
{
    public static MenuKeyboard instance;
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
        float input = menuInput.Menu.Navigate.ReadValue<float>();

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

        menuInput.Menu.Submit.started += InvokeButton;
        menuInput.Menu.Navigate.started += NavigateInput;
        menuInput.Menu.Esc.started += ExitPanel;
    }

    void OnDisable()
    {
        menuInput.Menu.Submit.started -= InvokeButton;
        menuInput.Menu.Navigate.started -= NavigateInput;
        menuInput.Menu.Esc.started -= ExitPanel;

        menuInput.Disable();
    }
}
