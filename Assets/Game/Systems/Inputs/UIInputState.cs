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
        //GameInput.UI.Navigate.started += Navigate;
        ///GameInput.UI.Submit.started += Submit;
        GameInput.UI.Esc.started += Esc;
    }

    public void Exit()
    {
        //GameInput.UI.Navigate.started -= Navigate;
        //GameInput.UI.Submit.started -= Submit;
        GameInput.UI.Esc.started -= Esc;
    }


    private void Navigate(InputAction.CallbackContext context)
    {
        //float input = context.ReadValue<float>();
        //int direction = (input == 1) ? -1 : 1;

        //var evt = new OnNavigate(direction);
        //eventBus.Invoke(evt);
    }

    private void Submit(InputAction.CallbackContext context)
    {
        //var evt = new OnNavigate(OnButtonInvoke);
        //eventBus.Invoke(evt);
    }

    private void Esc(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(UIInputEvents.Esc);
    }
}

public enum UIInputEvents
{
    Esc
}
