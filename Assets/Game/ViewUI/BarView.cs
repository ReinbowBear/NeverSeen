
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarView : MonoBehaviour
{
    public Image barImage;
    public Gradient barGradient;
    public float fillSpeed = 0.3f;


    public void SetValue(float current, float max)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateBar(current, max));
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
}