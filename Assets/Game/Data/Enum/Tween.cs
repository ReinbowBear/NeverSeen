using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class Tween
{
    public static Sequence Impact(Transform transform)
    {
        return DOTween.Sequence()
        .SetLink(transform.gameObject)
        .Append(transform.DOScale(new Vector3(0.95f, 1.1f, 0.95f), 0.25f))
        .Append(transform.DOScale(Vector3.one, 0.25f));
    }


    public static DG.Tweening.Tween Spawn(Transform transform, float duration = 0.3f)
    {
        transform.localScale = Vector3.zero;
        return transform.DOScale(Vector3.one, duration)
        .SetLink(transform.gameObject)
        .SetEase(Ease.OutBack);
    }

    public static DG.Tweening.Tween Destroy(Transform transform, float duration = 0.3f)
    {
        return transform.DOScale(Vector3.zero, duration)
        .SetLink(transform.gameObject)
        .SetEase(Ease.InBack)
        .OnComplete(() => transform.gameObject.SetActive(false));
    }


    public static DG.Tweening.Tween Shake(Transform transform, float duration = 0.3f, float strength = 10f)
    {
        return transform.DOShakePosition(duration, strength)
        .SetLink(transform.gameObject)
        .SetEase(Ease.OutQuad);
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

    public static DG.Tweening.Tween Pulse(Transform transform, float scaleAmount = 1.1f, float speed = 0.6f, int count = 1) // pulses -1 будет бессконечностью
    {
        return transform.DOScale(Vector3.one * scaleAmount, speed / 2)
        .SetLink(transform.gameObject)
        .SetLoops(count, LoopType.Yoyo)
        .SetEase(Ease.InOutSine);
    }


    public static DG.Tweening.Tween MoveFromCenter(Transform transform, Vector3 offset, float duration = 0.5f)
    {
        Vector3 targetPosition = transform.position + offset;

        return transform.DOMove(targetPosition, duration / 2)
        .SetLink(transform.gameObject)
        .SetEase(Ease.InOutSine);
    }



    public static Sequence FloatText(Transform transform, float moveY = 1f, float duration = 0.5f)
    {
        return DOTween.Sequence()
        .SetLink(transform.gameObject)
        .Join(transform.DOMoveY(transform.position.y + moveY, duration))
        .Join(transform.DOScale(1.2f, duration * 0.5f).SetLoops(2, LoopType.Yoyo))
        .Join(transform.GetComponent<CanvasGroup>().DOFade(0, duration))
        .OnComplete(() => GameObject.Destroy(transform.gameObject));
    }

    public static DG.Tweening.Tween Fade(CanvasGroup group, float value, float duration = 0.5f, bool isActive = true)
    {
        return group.DOFade(value, duration)
        .SetLink(group.gameObject)
        .OnComplete(() => group.gameObject.SetActive(isActive));
    }
}
