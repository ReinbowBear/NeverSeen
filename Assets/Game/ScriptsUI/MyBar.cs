using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyBar : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image barImage;
    [SerializeField] private TextMeshProUGUI barText;

    [Header("Animation Settings")]
    [SerializeField] private float fillSpeed;
    //[SerializeField] private float fadeTimeOut;
    //[SerializeField] private float fadeSpeed;

    [Header("Color Gradient")]
    [SerializeField] private Gradient barGradient;

    private CanvasGroup canvasGroup;
    private Coroutine fillCoroutine;
    private Coroutine valueCoroutine;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void SetBarValue(float current, float max, bool instant = false)
    {
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }
        fillCoroutine = StartCoroutine(AnimateBar(current, max, instant));

        if (valueCoroutine != null)
        {
            StopCoroutine(valueCoroutine);
        }
        valueCoroutine = StartCoroutine(AnimateNumber(current, instant));

        //FadeBar(1);
    }

    private IEnumerator AnimateBar(float current, float max, bool instant)
    {
        float targetFill = Mathf.Clamp01(current / max);
        if (barImage == null) yield break;

        if (instant)
        {
            barImage.fillAmount = targetFill;
            barImage.color = barGradient.Evaluate(targetFill);
        }
        else
        {
            while (!Mathf.Approximately(barImage.fillAmount, targetFill))
            {
                barImage.fillAmount = Mathf.MoveTowards(barImage.fillAmount, targetFill, fillSpeed * Time.deltaTime);
                barImage.color = barGradient.Evaluate(barImage.fillAmount);
                yield return null;
            }
        }
    }

    private IEnumerator AnimateNumber(float targetValue, bool instant)
    {
        if (barText == null) yield break;

        float currentValue = 0f;
        float.TryParse(barText.text, out currentValue);

        if (instant)
        {
            barText.text = Mathf.CeilToInt(targetValue).ToString();
        }
        else
        {
            while (!Mathf.Approximately(currentValue, targetValue))
            {
                currentValue = Mathf.MoveTowards(currentValue, targetValue, fillSpeed * Time.deltaTime);
                barText.text = Mathf.CeilToInt(currentValue).ToString();
                yield return null;
            }
        }

        //yield return new WaitForSeconds(fadeTimeOut);
        //FadeBar(0);
    }


    //public void FadeBar(float targetAlpha)
    //{
    //    if (fadeCoroutine != null)
    //    {
    //        StopCoroutine(fadeCoroutine);
    //    }
    //    fadeCoroutine = StartCoroutine(FadeCanvasGroup(targetAlpha));
    //}
//
    //private IEnumerator FadeCanvasGroup(float targetAlpha)
    //{
    //    while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
    //    {
    //        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
    //        canvasGroup.interactable = canvasGroup.blocksRaycasts = targetAlpha > 0.9f;
    //        yield return null;
    //    }
    //}
}
