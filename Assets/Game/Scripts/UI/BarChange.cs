using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarChange : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI ImageText;
    
    public void ChangeBar(float maxValue, float currentValue)
    {
        if (ImageText != null)
        {
            ImageText.text = currentValue.ToString();
        }

        float barChange = currentValue / maxValue;
        image.fillAmount = barChange;
    }
}
