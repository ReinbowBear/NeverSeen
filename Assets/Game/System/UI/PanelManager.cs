using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private Panel rootPanel;
    public Stack<Panel> Panels { get; private set; } = new();
    private Panel currentPanel => Panels.Count > 0 ? Panels.Peek() : null;
    private Input input;

    [Inject]
    public void Construct(Input input)
    {
        this.input = input;
    }

    void Start()
    {
        input.UI.Submit.started += InvokeButton;
        input.UI.Esc.started += ClosePanel;
    }


    private void InvokeButton(InputAction.CallbackContext _)
    {
        if (currentPanel == null) return;

        var selectedObj = EventSystem.current.currentSelectedGameObject;
        if (selectedObj == null) return;

        var button = selectedObj.GetComponent<Button>();
        if (button == null) return;

        button.onClick.Invoke();
    }

    [EventHandler(Priority.low)]
    private void OnPanelTrugered(OnPanelOpen panelEvent)
    {
        if (panelEvent.isOpen) OpenPanel(panelEvent.panel);
        else ClosePanel();
    }


    private void OpenPanel(Panel panel)
    {
        if (currentPanel != null)
        {
            currentPanel.SetNavigation(false);
        }
        else if (rootPanel != null)
        {
            rootPanel.SetNavigation(false);
        }

        Panels.Push(panel);
    }

    private void ClosePanel(InputAction.CallbackContext _ = default)
    {
        if (currentPanel != null)
        {
            Panels.Pop();
        }

        if (currentPanel != null)
        {
            currentPanel.SetNavigation(true);
        }
        else if (rootPanel != null)
        {
            rootPanel.SetNavigation(true);
        }
    }
}
