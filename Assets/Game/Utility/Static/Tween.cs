using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public static class Tween
{
    #region GameObject
    public static Sequence Impact(Transform transform, float power = 0.1f, float time = 0.5f)
    {
        return DOTween.Sequence()
        .SetLink(transform.gameObject)
        .Append(transform.DOScale(new Vector3(1f - power, 1f + power * 2, 1f - power), time / 2))
        .Append(transform.DOScale(Vector3.one, time / 2));
    }
    #endregion

    #region SFX
    public static DG.Tweening.Tween Shake(Transform transform, float duration = 0.3f, float strength = 10f)
    {
        return transform.DOShakePosition(duration, strength)
        .SetLink(transform.gameObject)
        .SetEase(Ease.OutQuad);
    }

    public static Sequence Blink(SpriteRenderer spriteRenderer, int blinks = 2, float blinkDuration = 0.1f)
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
    #endregion

    #region Transformation
    public static DG.Tweening.Tween MoveToPosition(Transform transform, Vector3 targetPosition, float duration = 0.5f)
    {
        return transform.DOMove(targetPosition, duration)
        .SetLink(transform.gameObject)
        .SetEase(Ease.InOutSine);
    }
    #endregion

    #region Text
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
    #endregion

    #region Audio
    public static DG.Tweening.Tween FadeVolume(AudioSource source, float toVolume, float duration = 0.5f)
    {
        return DOTween.To(() => source.volume, value => source.volume = value, toVolume, duration)
        .SetEase(Ease.InOutSine)
        .SetLink(source.gameObject);
    }

    public static DG.Tweening.Tween FadeMixerVolume(AudioMixer mixer, string parameter, float toVolume, float duration = 0.5f)
    {
        float targetDb = Mathf.Log10(Mathf.Clamp(toVolume, 0.0001f, 1f)) * 20f;
        float currentDb;

        mixer.GetFloat(parameter, out currentDb);

        return DOTween.To(() => currentDb, x => { currentDb = x; mixer.SetFloat(parameter, currentDb); }, targetDb, duration)
        .SetEase(Ease.InOutSine);
    }

    public static DG.Tweening.Tween FadePitch(AudioSource source, float toPitch, float duration = 0.5f)
    {
        return DOTween.To(() => source.pitch, value => source.pitch = value, toPitch, duration)
        .SetEase(Ease.InOutSine)
        .SetLink(source.gameObject);
    }

    public static DG.Tweening.Sequence MutingVolume(AudioMixer mixer, string parameter, float targetVolume, float duration)
    {
        mixer.GetFloat(parameter, out float originalDb);
        float targetDb = Mathf.Log10(Mathf.Clamp(targetVolume, 0.0001f, 1f)) * 20f; // targetDb это децибелы

        return DOTween.Sequence()
        .Append(DOTween.To(() => originalDb, x => mixer.SetFloat(parameter, x), targetDb, duration / 2)
        .SetEase(Ease.InOutSine))

        .Append(DOTween.To(() => targetDb, x => mixer.SetFloat(parameter, x), originalDb, duration / 2)
        .SetEase(Ease.InOutSine));
    }
    #endregion
}
