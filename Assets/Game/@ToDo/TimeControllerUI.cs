using UnityEngine;
using UnityEngine.UI;

public class TimeControllerUI : MonoBehaviour
{
    [SerializeField] private Color Offcolor;
    [SerializeField] private Color OnColor;
    [Space]
    [SerializeField] private UnityEngine.UI.Button[] buttons;
    private UnityEngine.UI.Button currentButton;

    public void SetTime(float timeValue, UnityEngine.UI.Button button)
    {
        Time.timeScale = timeValue;

        var colorBlock = currentButton.colors;
        colorBlock.normalColor = Offcolor;
        currentButton.colors = colorBlock;

        colorBlock = button.colors;
        colorBlock.normalColor = OnColor;
        button.colors = colorBlock;

        currentButton = button;
    }
}