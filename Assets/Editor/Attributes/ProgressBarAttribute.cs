using UnityEngine;

public class ProgressBarAttribute : PropertyAttribute
{
    public readonly float Min;
    public readonly float Max;
    public readonly string Label;

    public ProgressBarAttribute(float min, float max, string label = null)
    {
        Min = min;
        Max = max;
        Label = label;
    }
}
