using UnityEngine;

public class ProxyPanelControl : BaseProxy
{
    public Panel rootPanel;
    private PanelControl panelControl = new();

    public override void Init()
    {
        foreach (var buttonEvt in FindObjectsByType<ButtonEvents>(default))
        {
            buttonEvt.GetComponent<ButtonEvents>().SetEventBus(eventWorld);
        }
    }

    public override void Enter()
    {
        eventWorld.AddListener(ClosePanel, UIInputEvents.Esc);
        eventWorld.AddListener<Transform>(NavigateInput, UIEvents.OnNavigate);
    }

    public override void Exit()
    {
        eventWorld.RemoveListener(ClosePanel, UIInputEvents.Esc);
        eventWorld.RemoveListener<Transform>(NavigateInput, UIEvents.OnNavigate);
    }


    private void NavigateInput(Transform newTarget)
    {
        if(panelControl.CurrentPanel != null && !panelControl.CurrentPanel.TryGetComponent<PanelNavigate>(out var navigate)) 
        {
            navigate.MoveTo(newTarget.position);
        }
        else
        {
            rootPanel.GetComponent<PanelNavigate>().MoveTo(newTarget.position);
        }
    }


    public void OpenPanel(Panel panel)
    {
        panelControl.OpenPanel(panel);
        eventWorld.Invoke(UIEvents.OnPanelOpen);
    }

    public void ClosePanel()
    {
        if(panelControl.ClosePanel(out var closed)) return;
        eventWorld.Invoke(UIEvents.OnPanelClose);
    }
}
