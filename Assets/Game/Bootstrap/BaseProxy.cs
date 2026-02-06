using UnityEngine;

public abstract class BaseProxy : MonoBehaviour, IState
{
    protected EventWorld eventWorld;

    public void SetEventBus(EventWorld eventWorld)
    {
        this.eventWorld = eventWorld;
    }


    public virtual void Init() { }
    public virtual void Enter() { }
    public virtual void Exit() { }
}
