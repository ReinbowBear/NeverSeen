using DG.Tweening;
using UnityEngine;

public class SpawnView : MonoBehaviour, IActivatable
{
    public float duration = 0.3f;

    public void Activate()
    {
        transform.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;

        transform.DOScale(Vector3.one, duration)
        .SetLink(transform.gameObject)
        .SetEase(Ease.OutBack);
    }

    public void Deactivate()
    {
        transform.DOScale(Vector3.zero, duration)
        .SetLink(transform.gameObject)
        .SetEase(Ease.InBack)
        .OnComplete(() => transform.gameObject.SetActive(false));
    }
}
