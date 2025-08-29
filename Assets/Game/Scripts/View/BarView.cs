using UnityEngine;

public class BarView : MonoBehaviour, IBarView
{
    [SerializeField] private MyBar bar;
    [Space]
    [SerializeField] private float startValue;
    [SerializeField] private bool isRotate;

    void Start()
    {
        bar.StartedValue(startValue, isRotate);
    }


    public void DrawBar(bool isActive)
    {
        bar.FadeBar(isActive ? 1f : 0f);
    }

    public void ChangeValue(int value, int maxValue)
    {
        bar.SetValue(value, maxValue);
    }
}
