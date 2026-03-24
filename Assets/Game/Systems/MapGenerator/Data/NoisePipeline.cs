using UnityEngine;

[System.Serializable]
public class NoisePipeline
{
    [Header("Warp Noise")]
    public float WarpScale;
    public int WarpDetailLevel;
    public float WarpDetailStrength;
    public float WarpFluctuations;

    [Header("Warped Noise")]
    public float warpStrength;

    [Header("Mask Noise")]
    public Vector2 offset;
    public float radius;
    public float falloff;

    public INoise GetResult(int seed, INoise continentNoise)
    {
        var warp = new Noise(seed, WarpScale, WarpDetailLevel, WarpDetailStrength, WarpFluctuations);
        var warped = new WarpedNoise(continentNoise, warp, warpStrength);
        var mask = new MaskNoise(warped, offset, radius, falloff);
        return mask;
    }
}
