using UnityEngine;

public class ClampNoise : INoise // Когда смешиваем несколько шумов с разными масштабами, итоговое значение может выйти за пределы [0,1]
{
    private INoise baseNoise;
    private float min;
    private float max;

    public ClampNoise(INoise baseNoise, float min, float max)
    {
        this.baseNoise = baseNoise;
        this.min = min;
        this.max = max;
    }

    public float GetHeight(float x, float y)
    {
        return Mathf.Clamp(baseNoise.GetHeight(x, y), min, max);
    }
}
