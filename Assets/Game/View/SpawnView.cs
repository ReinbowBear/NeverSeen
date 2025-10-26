using DG.Tweening;
using UnityEngine;

public class SpawnView : MonoBehaviour
{
    public float duration = 0.3f;

    public void Spawn()
    {
        transform.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;

        transform.DOScale(Vector3.one, duration)
        .SetLink(transform.gameObject)
        .SetEase(Ease.OutBack);
    }

    public void Destroy()
    {
        transform.DOScale(Vector3.zero, duration)
        .SetLink(transform.gameObject)
        .SetEase(Ease.InBack)
        .OnComplete(() => transform.gameObject.SetActive(false));
    }
}
