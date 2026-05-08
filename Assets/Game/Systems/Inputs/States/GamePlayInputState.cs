using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayInputState : ISystem, IState
{
    private GameInput.GamePlayActions actions;

    public GamePlayInputState(GameInput.GamePlayActions actions)
    {
        this.actions = actions;
    }


    public void Execute(World world, EntityCommands commands)
    {
        HandleMouse(commands);

        HandleCameraMove(commands);
        HandleRotation(commands);
        HandleZoom(commands);
    }



    private void HandleMouse(EntityCommands commands)
    {
        var mousePos = Mouse.current.position.ReadValue();

        if (actions.MouseLeft.WasPressedThisFrame())
        {
            var intent = new IntentLeftClick(mousePos);
            commands.AddOneFrame(intent);
        }

        if (actions.MouseRight.WasPressedThisFrame())
        {
            var intent = new IntentRightClick(mousePos);
            commands.AddOneFrame(intent);
        }
    }


    private void HandleCameraMove(EntityCommands commands)
    {
        var move = actions.WASD.ReadValue<Vector2>();

        if (move.sqrMagnitude > 0.001f)
        {
            var intent = new IntentMove(move, actions.Shift.IsPressed());
            commands.AddOneFrame(intent);
        }
    }

    private void HandleRotation(EntityCommands commands)
    {
        int rotate = 0;

        if (actions.Q.IsPressed()) rotate -= 1;
        if (actions.E.IsPressed()) rotate += 1;

        if (Mathf.Abs(rotate) > 0.01f)
        {
            var intent = new IntentCameraRotate(rotate);
            commands.AddOneFrame(intent);
        }
    }

    private void HandleZoom(EntityCommands commands)
    {
        var scroll = actions.Scroll.ReadValue<Vector2>();

        if (Mathf.Abs(scroll.y) > 0.01f)
        {
            var intent = new IntentScroll(scroll);
            commands.AddOneFrame(intent);
        }
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
