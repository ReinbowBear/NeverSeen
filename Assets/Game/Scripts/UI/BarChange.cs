using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarChange : MonoBehaviour
{
    public Image icon;
    [SerializeField] private Image barImage;
    [SerializeField] private TextMeshProUGUI barText;
    
    public void ChangeBar(float maxValue, float currentValue)
    {
        barText.text = currentValue.ToString();

        if (barImage != null)
        {
            float barChange = currentValue / maxValue;
            barImage.fillAmount = barChange;
        }
    }
}
