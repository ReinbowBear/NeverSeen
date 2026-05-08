using UnityEngine;

public struct IntentMove
{
    public Vector2 Direction;
    public bool IsRunning; // shift

    public IntentMove(Vector2 direction, bool isRunning)
    {
        Direction = direction;
        IsRunning = isRunning;
    }
}
