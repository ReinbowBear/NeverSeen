using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarChange : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private TextMeshProUGUI barText;
    
    public void ChangeBar(float maxValue, float currentValue)
    {
        if (barImage != null)
        {
            float barChange = currentValue / maxValue;
            barImage.fillAmount = barChange;
        }
        
        if (barText != null)
        {
            barText.text = Mathf.CeilToInt(currentValue).ToString();
        }
    }
}
