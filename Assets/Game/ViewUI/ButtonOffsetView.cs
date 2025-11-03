using UnityEngine;

public class ButtonOffsetView : MonoBehaviour
{
    [Header("Ref")]
    public PointerEnter pointerEnter;

    [Header("Settings")]
    public float animationOffset = 50;
    public float animationTime = 0.2f;
    private Vector3 originalPos;

    void Awake()
    {
        originalPos = pointerEnter.transform.position;
    }


    private void OnButtonEnter()
    {
        Tween.MoveToPosition(pointerEnter.transform, originalPos + new Vector3(animationOffset, 0, 0), animationTime);
    }

    private void OnButtonExit()
    {
        Tween.MoveToPosition(pointerEnter.transform, originalPos, animationTime);
    }
}
