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
        eventWorld.AddListener(ClosePanel, Events.UIInput.Esc);
        eventWorld.AddListener<Transform>(NavigateInput, Events.UIEvents.OnNavigate);
    }

    public override void Exit()
    {
        eventWorld.RemoveListener(ClosePanel, Events.UIInput.Esc);
        eventWorld.RemoveListener<Transform>(NavigateInput, Events.UIEvents.OnNavigate);
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
        eventWorld.Invoke(Events.UIEvents.OnPanelOpen);
    }

    public void ClosePanel()
    {
        if(panelControl.ClosePanel(out var closed)) return;
        eventWorld.Invoke(Events.UIEvents.OnPanelClose);
    }
}
