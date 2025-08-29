using DG.Tweening;
using UnityEngine;

public static class TweenAnimation
{
    public static Sequence Impact(Transform transform)
    {
        return DOTween.Sequence()
        .SetLink(transform.gameObject)
        .Append(transform.DOScale(new Vector3(0.95f, 1.1f, 0.95f), 0.25f))
        .Append(transform.DOScale(Vector3.one, 0.25f));
    }


    public static Tween Spawn(Transform transform, float duration = 0.3f)
    {
        transform.localScale = Vector3.zero;
        return transform.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);
    }

    public static Tween Destroy(Transform transform, float duration = 0.2f)
    {
        return transform.DOScale(Vector3.zero, duration).SetEase(Ease.InBack);
    }


    public static Tween Shake(Transform transform, float duration = 0.3f, float strength = 10f)
    {
        return transform.DOShakePosition(duration, strength).SetEase(Ease.OutQuad);
    }

    public static Sequence Blink(SpriteRenderer spriteRenderer, int blinks = 3, float blinkDuration = 0.1f)
    {
        Sequence sequence = DOTween.Sequence().SetLink(spriteRenderer.gameObject);
        for (int i = 0; i < blinks; i++)
        {
            sequence.Append(spriteRenderer.DOFade(0, blinkDuration));
            sequence.Append(spriteRenderer.DOFade(1, blinkDuration));
        }
        return sequence;
    }

    public static Tween Pulse(Transform transform, float scaleAmount = 1.1f, float duration = 0.6f)
    {
        return transform.DOScale(Vector3.one * scaleAmount, duration / 2)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutSine);
    }

    public static Tween Sway(Transform transform, float angle = 15f, float duration = 1f)
    {
        return transform.DORotate(new Vector3(0, 0, angle), duration / 2)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutSine);
    }


    public static Sequence FloatText(Transform transform, float moveY = 1f, float duration = 1f)
    {
        return DOTween.Sequence()
        .SetLink(transform.gameObject)
        .Join(transform.DOMoveY(transform.position.y + moveY, duration))
        .Join(transform.DOScale(1.2f, duration * 0.5f).SetLoops(2, LoopType.Yoyo))
        .Join(transform.GetComponent<CanvasGroup>().DOFade(0, duration))
        .OnComplete(() => GameObject.Destroy(transform.gameObject));
    }

    public static Tween FadeIn(CanvasGroup group, float duration = 0.3f)
    {
        group.alpha = 0;
        group.gameObject.SetActive(true);
        return group.DOFade(1, duration);
    }

    public static Tween FadeOut(CanvasGroup group, float duration = 0.3f)
    {
        return group.DOFade(0, duration).OnComplete(() => group.gameObject.SetActive(false));
    }
}
