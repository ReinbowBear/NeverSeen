using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Image Image;
    public float Value;

    private bool isActive;
    private Color originalColor;

    void Awake()
    {
        originalColor = Image.color;
    }


    public void Toggle()
    {
        isActive = !isActive;

        if (isActive)
        {
            Image.color = new Color(
            originalColor.r * Value,
            originalColor.g * Value,
            originalColor.b * Value);
        }
        else
        {
            Image.color = originalColor;
        }
    }
}
