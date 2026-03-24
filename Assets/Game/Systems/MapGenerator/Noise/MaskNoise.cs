using Unity.Mathematics;
using UnityEngine;

public class MaskNoise : INoise
{
    private INoise baseNoise;
    private float radius;
    private float falloff;

    private float2 offset;

    public MaskNoise(INoise baseNoise, float2 offset, float radius = 100f, float falloff = 2f)
    {
        this.baseNoise = baseNoise;
        this.offset = offset;

        this.radius = radius;
        this.falloff = falloff;
    }


    public float GetHeight(float x, float y)
    {
        float posX = x - offset.x;
        float posY = y - offset.y;

        float baseValue = baseNoise.GetHeight(x, y);

        float distance = Mathf.Sqrt(posX * posX + posY * posY);
        float mask = 1f - Mathf.Pow(distance / radius, falloff);

        mask = Mathf.Clamp01(mask);

        return baseValue * mask;
    }
}
