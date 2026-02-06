using UnityEngine;

public class ProxyState : MonoBehaviour, IState
{
    public BaseProxy[] proxys;

    public void Init(EventWorld eventWorld, DependencyResolver resolver)
    {
        foreach (var proxy in proxys)
        {
            proxy.SetEventBus(eventWorld);
            resolver.Resolve(proxy);
            proxy.Init();
        }
    }


    public void Enter()
    {
        foreach (var proxy in proxys)
        {
            proxy.Enter();
        }
    }

    public void Exit()
    {
        foreach (var proxy in proxys)
        {
            proxy.Exit();
        }
    }
}
