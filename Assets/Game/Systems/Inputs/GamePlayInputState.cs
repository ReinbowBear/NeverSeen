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
        GameInput.GamePlay.MouseRight.canceled += MouseRightCanceled;
        GameInput.GamePlay.Q.started += Q;
        GameInput.GamePlay.E.started += E;
        GameInput.GamePlay.Scroll.started += Scroll;
        GameInput.GamePlay.Shift.started += Shift;
        GameInput.GamePlay.WASD.started += WASD;
    }

    public void Exit()
    {
        GameInput.GamePlay.MouseLeft.started -= MouseLeft;
        GameInput.GamePlay.MouseRight.started -= MouseRight;
        GameInput.GamePlay.MouseRight.canceled -= MouseRightCanceled;
        GameInput.GamePlay.Q.started -= Q;
        GameInput.GamePlay.E.started -= E;
        GameInput.GamePlay.Scroll.started -= Scroll;
        GameInput.GamePlay.Shift.started -= Shift;
        GameInput.GamePlay.WASD.started -= WASD;
    }


    private void MouseLeft(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.LeftClick);
    }

    private void MouseRight(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.RightClick);
    }

    private void MouseRightCanceled(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.RightClickCancel);
    }

    private void Q(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.Q);
    }

    private void E(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.E);
    }

    private void Scroll(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.Scroll);
    }

    private void Shift(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.Shift);
    }

    private void WASD(InputAction.CallbackContext context)
    {
        eventWorld.Invoke(Events.GamePlayInput.WASD);
    }
}
