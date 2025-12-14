using DG.Tweening;
using UnityEngine;

public class OffsetView : MonoBehaviour, IActivatable
{
    public Vector3 showPos;
    public Vector3 HidePos;
    [Space]
    public float animationTime = 0.2f;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    public void Activate()
    {
        MoveToPosition(showPos);
    }

    public void Deactivate()
    {
        MoveToPosition(HidePos);
    }


    private void MoveToPosition(Vector3 targetPosition)
    {
        rectTransform.DOAnchorPos(targetPosition, animationTime)
        .SetLink(gameObject)
        .SetEase(Ease.InOutSine);
    }
}
