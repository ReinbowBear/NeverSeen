using DG.Tweening;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [Space]
    [SerializeField] private float time;
    [SerializeField] private Vector3 hidePos;

    private Vector3 basePos;
    private Sequence sequence;

    void Awake()
    {
        basePos = rect.position;
    }

    void Start()
    {
        Hide();
    }


    private void Show()
    {
        if (sequence != null)
        {
            sequence.Kill();
        }

        gameObject.SetActive(true);

        sequence = DOTween.Sequence()
            .Append(rect.DOAnchorPos3D(basePos, time))
            .OnComplete(() => { sequence = null; });
    }

    private void Hide()
    {
        if (sequence != null)
        {
            sequence.Kill();
        }

        sequence = DOTween.Sequence()
            .Append(rect.DOAnchorPos3D(hidePos, time))
            .OnComplete(() => { gameObject.SetActive(false); sequence = null; });
    }

    void Onestroy()
    {
        if (sequence != null)
        {
            sequence.Kill();
        }
    }
}
