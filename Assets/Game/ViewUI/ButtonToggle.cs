using UnityEngine;
using UnityEngine.UI;
public class ButtonToggle : MonoBehaviour
{
    public Image Image;
    [Range(0, 1)] public float Value;

    private Color originalColor;
    private bool isActive;

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
