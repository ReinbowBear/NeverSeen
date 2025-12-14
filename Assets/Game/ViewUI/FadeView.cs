using System.Collections;
using UnityEngine;

public class FadeView : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeSpeed = 0.3f;


    public void FadeBar(bool isVisible)
    {
        var value = isVisible ? 1f : 0f;

        StopAllCoroutines();
        StartCoroutine(FadeCanvasGroup(value));
    }

    public void FadeAfterDelay(bool isVisible, float delay)
    {
        var value = isVisible ? 1f : 0f;

        StopAllCoroutines();
        StartCoroutine(FadeAfter(value, delay));
    }


    private IEnumerator FadeAfter(float targetAlpha, float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return FadeCanvasGroup(targetAlpha);
    }

    private IEnumerator FadeCanvasGroup(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeSpeed)
        {
            elapsed += Time.deltaTime;
            float time = Mathf.Clamp01(elapsed / fadeSpeed);

            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time);
            canvasGroup.interactable = canvasGroup.blocksRaycasts = targetAlpha > 0.9f; // тут кстати if проверка своеобразная
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.interactable = canvasGroup.blocksRaycasts = targetAlpha > 0.9f;
    }
}