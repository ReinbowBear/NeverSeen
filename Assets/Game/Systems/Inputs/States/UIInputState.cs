
public class UIInputState : IUpdate, IState
{
    private GameInput.UIActions actions;
    private UIInput inputComp;

    public UIInputState(GameInput.UIActions actions, UIInput inputComp)
    {
        this.actions = actions;
        this.inputComp = inputComp;
    }


    public void Update(World world)
    {
        inputComp.Esc = actions.Esc.IsPressed();
    }


    public void Enter()
    {
        actions.Enable();
    }

    public void Exit()
    {
        actions.Disable();
    }
}


public class UIInput
{
    public bool Esc;
}
