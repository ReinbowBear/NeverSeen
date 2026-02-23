using UnityEngine;

public class ProxyNavigate : MonoBehaviour, IEventListener
{
    private PanelStack panelStack;

    public void SetEvents(EventWorld eventWorld)
    {
        eventWorld.AddListener<Transform>(this, NavigateInput, Events.UIEvents.OnNavigate);
    }

    private void NavigateInput(Transform newTarget)
    {
        if(panelStack.CurrentPanel != null && !panelStack.CurrentPanel.TryGetComponent<PanelNavigate>(out var navigate)) 
        {
            navigate.MoveTo(newTarget.position);
        }
        else
        {
            panelStack.root.GetComponent<PanelNavigate>().MoveTo(newTarget.position);
        }
    }
}
