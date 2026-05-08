using UnityEngine;

public class MouseInput : ISystem
{
    private Vector2 lastPos;
    private MouseState mouseState;

    public MouseInput(MouseState mouseState)
    {
        this.mouseState = mouseState;
    }


    public void Execute(World world, EntityCommands commands)
    {
        HandleMouseData(mouseState);
        HandleLeftClick(commands);
        HandleRightClick(commands);
    }


    private void HandleMouseData(MouseState state)
    {
        Vector2 pos = UnityEngine.Input.mousePosition;
        Vector2 delta = pos - lastPos;
        lastPos = pos;

        bool moved = delta.sqrMagnitude > 0.001f;

        state.Position = pos;
        state.Delta = delta;
        state.IsMoved = moved;
    }

    private void HandleLeftClick(EntityCommands commands)
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            var intent = new IntentLeftClick();
            commands.AddOneFrame(intent);
        }
    }

    private void HandleRightClick(EntityCommands commands)
    {
        if (UnityEngine.Input.GetMouseButtonDown(1))
        {
            var intent = new IntentRightClick();
            commands.AddOneFrame(intent);
        }
    }
}

public class MouseState
{
    public Vector2 Position;
    public Vector2 Delta;
    public bool IsMoved;
}
