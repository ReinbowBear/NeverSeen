using UnityEngine;

public struct IntentRightClick
{
    public Vector2 ClickPosition;

    public IntentRightClick(Vector2 screenPosition)
    {
        ClickPosition = screenPosition;
    }
}
