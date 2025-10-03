using UnityEngine;

public class ButtonOffsetView : MonoBehaviour
{
    [Header("Ref")]
    public MyButton button;

    [Header("Settings")]
    public float animationOffset = 50;
    public float animationTime = 0.2f;
    private Vector3 originalPos;

    void Awake()
    {
        originalPos = button.transform.position;
    }


    private void OnButtonEnter()
    {
        Tween.MoveToPosition(button.transform, originalPos + new Vector3(animationOffset, 0, 0), animationTime);
    }

    private void OnButtonExit()
    {
        Tween.MoveToPosition(button.transform, originalPos, animationTime);
    }


    void OnEnable()
    {
        button.OnButtonEnter += OnButtonEnter;
        button.OnButtonExit += OnButtonExit;
    }

    void OnDisable()
    {
        button.OnButtonEnter -= OnButtonEnter;
        button.OnButtonExit -= OnButtonExit;
    }
}
