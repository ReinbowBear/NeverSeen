using UnityEngine;

public class PanelOffsetView : MonoBehaviour
{
    [Header("Ref")]
    public RectTransform Target;

    [Header("Settings")]
    public bool IsActive;
    public float AnimationTime = 0.2f;
    public Vector3 ShowPos;
    public Vector3 HidePos;


    public void Toggle()
    {
        IsActive = !IsActive;

        if (IsActive)
        {
            Tween.MoveToPosition(Target.transform, HidePos, AnimationTime);
        }
        else
        {
            Tween.MoveToPosition(Target.transform, ShowPos, AnimationTime);
        }
    }
}
