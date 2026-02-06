
public class ProxyInput : BaseProxy
{
    public InputMode StartedMode;
    private Input Input = new();

    public override void Init()
    {
        Input.Init(eventWorld);
        Input.SwitchTo(StartedMode);
    }

    public override void Exit()
    {
        Input.Dispose();
    }
}
