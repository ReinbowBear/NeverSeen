using DG.Tweening;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private float time;

    private Sequence sequence;


    private void Show()
    {
        OnDestroy();

        gameObject.SetActive(true);

        sequence = DOTween.Sequence()
            .Append(rect.DOScale(1.1f, time * 0.9f))
            .Append(rect.DOScale(1, time * 0.1f))
            .OnComplete(() => { sequence = null; });
    }

    private void Hide()
    {
        OnDestroy();

        sequence = DOTween.Sequence()
            .Append(rect.DOScale(1.1f, time * 0.1f))
            .Append(rect.DOScale(0, time * 0.9f))
            .OnComplete(() => { gameObject.SetActive(false); sequence = null; });
    }

    void OnDestroy()
    {
        if (sequence != null)
        {
            sequence.Kill();
        }
    }
}
