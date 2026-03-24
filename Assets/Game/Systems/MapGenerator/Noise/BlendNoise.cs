using UnityEngine;

public class BlendNoise : INoise
{
    private INoise noiseA;
    private INoise noiseB;
    private INoise blendMask;

    public BlendNoise(INoise a, INoise b, INoise mask)
    {
        noiseA = a;
        noiseB = b;
        blendMask = mask;
    }


    public float GetHeight(float x, float y)
    {
        float mask = blendMask.GetHeight(x, y);

        float a = noiseA.GetHeight(x, y);
        float b = noiseB.GetHeight(x, y);

        return Mathf.Lerp(a, b, mask);
    }
}
