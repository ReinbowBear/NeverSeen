using UnityEngine.InputSystem;

public class UIInputState : IState
{
    private GameInput GameInput;
    private EventWorld eventWorld;

    public UIInputState(GameInput GameInput, EventWorld eventWorld)
    {
        this.GameInput = GameInput;
        this.eventWorld = eventWorld;
    }


    public void Enter()
    {
        GameInput.UI.Esc.started += Esc;
    }

    public void Exit()
    {
        GameInput.UI.Esc.started -= Esc;
    }


    private void Esc(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.UIInput.Esc);
    }
}
