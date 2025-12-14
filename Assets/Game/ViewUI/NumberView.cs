using System.Collections;
using TMPro;
using UnityEngine;

public class NumberView : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public float fillSpeed;


    public void SetNumber(float targetValue)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateNumber(targetValue));
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
}
