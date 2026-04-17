using UnityEngine;

public class GamePlayInputState : IUpdate, IState
{
    private GameInput.GamePlayActions actions;
    private GamePlayInput inputComp;

    public GamePlayInputState(GameInput.GamePlayActions actions, GamePlayInput inputComp)
    {
        this.actions = actions;
        this.inputComp = inputComp;
    }


    public void Update(World world)
    {
        inputComp.MouseLeft = actions.MouseLeft.WasPressedThisFrame();
        inputComp.MouseRight = actions.MouseRight.WasPressedThisFrame();
        inputComp.Scroll = actions.Scroll.ReadValue<Vector2>();

        inputComp.Q = actions.Q.WasPressedThisFrame();
        inputComp.E = actions.E.WasPressedThisFrame();

        inputComp.WASD = actions.WASD.ReadValue<Vector2>();
        inputComp.Shift = actions.Shift.IsPressed();
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


public class GamePlayInput
{
    public bool MouseLeft;
    public bool MouseRight;
    public Vector2 Scroll;
    
    public bool Q;
    public bool E;

    public Vector2 WASD;
    public bool Shift;
}
