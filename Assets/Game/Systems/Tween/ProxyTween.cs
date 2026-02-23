using UnityEngine;
public class ProxyTween : MonoBehaviour, IEventListener
{
    private Tween tween = new();

    public void SetEvents(EventWorld eventWorld)
    {
        eventWorld.AddListener<Spawn>(this, tween.Spawn, Events.UIEvents.OnPanelOpen);
        eventWorld.AddListener<Destroy>(this, tween.Destroy, Events.UIEvents.OnPanelClose);
    }
}
