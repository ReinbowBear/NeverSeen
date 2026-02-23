using UnityEngine.InputSystem;

public class GamePlayInputState : IState
{
    private GameInput GameInput;
    private EventWorld eventWorld;

    public GamePlayInputState(GameInput GameInput, EventWorld eventWorld)
    {
        this.GameInput = GameInput;
        this.eventWorld = eventWorld;
    }


    public void Enter()
    {
        GameInput.GamePlay.MouseLeft.started += MouseLeft;
        GameInput.GamePlay.MouseRight.started += MouseRight;
        GameInput.GamePlay.Scroll.started += Scroll;

        GameInput.GamePlay.Q.started += Q;
        GameInput.GamePlay.E.started += E;
        
        GameInput.GamePlay.WASD.started += WASD;
        GameInput.GamePlay.Shift.started += Shift;
    }

    public void Exit()
    {
        GameInput.GamePlay.MouseLeft.started -= MouseLeft;
        GameInput.GamePlay.MouseRight.started -= MouseRight;
        GameInput.GamePlay.Scroll.started -= Scroll;

        GameInput.GamePlay.Q.started -= Q;
        GameInput.GamePlay.E.started -= E;

        GameInput.GamePlay.WASD.started -= WASD;
        GameInput.GamePlay.Shift.started -= Shift;
    }


    private void MouseLeft(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.LeftClick);
        eventWorld.Invoke(context, Events.GamePlayInput.LeftClick);
    }

    private void MouseRight(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.RightClick);
        eventWorld.Invoke(context, Events.GamePlayInput.RightClick);
    }

    private void Scroll(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.Scroll);
        eventWorld.Invoke(context, Events.GamePlayInput.Scroll);
    }


    private void Q(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.Q);
        eventWorld.Invoke(context, Events.GamePlayInput.Q);
    }

    private void E(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.E);
        eventWorld.Invoke(context, Events.GamePlayInput.E);
    }


    private void WASD(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.WASD);
        eventWorld.Invoke(context, Events.GamePlayInput.WASD);
    }

    private void Shift(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.Shift);
        eventWorld.Invoke(context, Events.GamePlayInput.Shift);
    }
}
