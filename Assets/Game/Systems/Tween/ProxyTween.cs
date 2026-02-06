using UnityEngine;

public class ProxyTween : BaseProxy
{
    private Tween tween = new();

    public override void Enter()
    {
        eventWorld.AddListener<Spawn>(tween.Spawn, Events.UIEvents.OnPanelOpen);
        eventWorld.AddListener<Destroy>(tween.Destroy, Events.UIEvents.OnPanelClose);
    }
}
