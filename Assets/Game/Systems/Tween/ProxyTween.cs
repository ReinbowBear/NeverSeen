using UnityEngine;
public class ProxyTween : MonoBehaviour, IEventListener
{
    private Tween tween = new();

    public void SetEvents(EventWorld eventWorld)
    {
        eventWorld.AddListener<Spawn>(tween.Spawn, Events.UIEvents.OnPanelOpen);
        eventWorld.AddListener<Destroy>(tween.Destroy, Events.UIEvents.OnPanelClose);
    }
}
