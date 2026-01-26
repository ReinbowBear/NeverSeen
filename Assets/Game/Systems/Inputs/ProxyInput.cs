
public class ProxyInput : BaseProxy
{
    public InputMode inputMode;
    private Input input = new();

    public override void Init()
    {
        input.Init(eventWorld);
        input.SwitchTo(inputMode);
    }

    public override void Exit()
    {
        input.Dispose();
    }
}
