using UnityEngine;

public class ProxyPanelControl : MonoBehaviour, IEventListener
{
    public Panel Root;
    private PanelStack panelStack;

    private EventWorld eventWorld;

    public ProxyPanelControl()
    {
        panelStack.root = Root;
    }

    public void SetEvents(EventWorld eventWorld)
    {
        eventWorld.AddListener(ClosePanel, Events.UIInput.Esc);
    }


    public void OpenPanel(Panel panel)
    {
        panelStack.OpenPanel(panel);
        eventWorld.Invoke(Events.UIEvents.OnPanelOpen);
    }

    public void ClosePanel()
    {
        if(!panelStack.ClosePanel()) return;
        eventWorld.Invoke(Events.UIEvents.OnPanelClose);
    }
}
