using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyBar : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image barImage;
    [SerializeField] private TextMeshProUGUI barText;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Animation Settings")]
    [SerializeField] private float fillSpeed;
    [SerializeField] private float fadeTimeOut;
    [SerializeField] private float fadeSpeed;

    [Header("Color Gradient")]
    [SerializeField] private Gradient barGradient;

    private Coroutine fillCoroutine;
    private Coroutine valueCoroutine;
    public Coroutine fadeCoroutine;

    public void StartedValue(float value)
    {
        barText.text = Mathf.CeilToInt(value).ToString();
    }


    public void SetValue(float current, float max)
    {
        canvasGroup.alpha = 1;

        if (fillCoroutine != null) { StopCoroutine(fillCoroutine); }

        if (barImage != null)
        {
            fillCoroutine = StartCoroutine(AnimateBar(current, max));
        }

        if (valueCoroutine != null) { StopCoroutine(valueCoroutine); }

        if (barText != null)
        {
            valueCoroutine = StartCoroutine(AnimateNumber(current));
        }
    }

    private IEnumerator AnimateBar(float current, float max)
    {
        float startFill = barImage.fillAmount;
        float targetFill = Mathf.Clamp01(current / max);
        float elapsed = 0f;

        while (elapsed < fillSpeed)
        {
            elapsed += Time.deltaTime;
            float time = Mathf.Clamp01(elapsed / fillSpeed);

            barImage.fillAmount = Mathf.Lerp(startFill, targetFill, time);
            barImage.color = barGradient.Evaluate(barImage.fillAmount);
            yield return null;
        }

        barImage.fillAmount = targetFill;
        barImage.color = barGradient.Evaluate(barImage.fillAmount);
        fillCoroutine = null;
    }

    private IEnumerator AnimateNumber(float targetValue)
    {
        float currentValue;
        float.TryParse(barText.text, out currentValue);

        float startValue = currentValue;
        float elapsed = 0f;

        while (elapsed < fillSpeed)
        {
            elapsed += Time.deltaTime;
            float time = Mathf.Clamp01(elapsed / fillSpeed);

            currentValue = Mathf.Lerp(startValue, targetValue, time);
            barText.text = Mathf.CeilToInt(currentValue).ToString();
            yield return null;
        }

        barText.text = Mathf.CeilToInt(targetValue).ToString();
        valueCoroutine = null;
    }


    public void FadeBar(float targetAlpha)
    {
        if (fadeCoroutine != null) { StopCoroutine(fadeCoroutine); }
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(targetAlpha));
    }

    private IEnumerator FadeCanvasGroup(float targetAlpha)
    {
        yield return fillCoroutine;
        yield return valueCoroutine;

        yield return new WaitForSeconds(fadeTimeOut);

        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeSpeed)
        {
            elapsed += Time.deltaTime;
            float time = Mathf.Clamp01(elapsed / fadeSpeed);

            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time);
            canvasGroup.interactable = canvasGroup.blocksRaycasts = targetAlpha > 0.9f;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.interactable = canvasGroup.blocksRaycasts = targetAlpha > 0.9f;
        fadeCoroutine = null;
    }
}
