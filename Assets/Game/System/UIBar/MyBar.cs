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
    [SerializeField] private RotateToCam rotate;

    [Header("Animation Settings")]
    [SerializeField] private float fillSpeed;
    [SerializeField] private float fadeTimeOut;
    [SerializeField] private float fadeSpeed;

    [Header("Color Gradient")]
    [SerializeField] private Gradient barGradient;

    private bool isRotate;

    public void Init(float value, bool isRotate = false)
    {
        barText.text = Mathf.CeilToInt(value).ToString();
        this.isRotate = isRotate;
    }


    public void SetValue(float current, float max)
    {
        canvasGroup.alpha = 1;

        if (barImage != null)
        {
            CoroutineManager.Start(AnimateBar(current, max), this);
        }

        if (barText != null)
        {
            CoroutineManager.Start(AnimateNumber(current), this);
        }

        if (isRotate)
        {
            rotate.enabled = true;
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
    }


    public void FadeBar(float targetAlpha)
    {
        CoroutineManager.Start(FadeCanvasGroup(targetAlpha), this);
    }

    private IEnumerator FadeCanvasGroup(float targetAlpha)
    {
        yield return CoroutineManager.Wait(AnimateBar(0, 0), this); // есть теория что вейт не работает так как запускает новую коррутину или ещё какую то херню
        yield return CoroutineManager.Wait(AnimateNumber(0), this);

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
        rotate.enabled = false; // подразумевается что прозрачность всегда 1 или 0 так что я не стал делать проверку
        canvasGroup.interactable = canvasGroup.blocksRaycasts = targetAlpha > 0.9f;
    }
}
