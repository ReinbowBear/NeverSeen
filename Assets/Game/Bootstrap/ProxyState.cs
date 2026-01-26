
public class ProxyState : BaseProxy
{
    public BaseProxy[] proxys;

    public override void Init()
    {
        foreach (var proxy in proxys)
        {
            proxy.SetEventBus(eventWorld);
            proxy.Init();
        }
    }


    public override void Enter()
    {
        foreach (var proxy in proxys)
        {
            proxy.Enter();
        }
    }

    public override void Exit()
    {
        foreach (var proxy in proxys)
        {
            proxy.Exit();
        }
    }
}
