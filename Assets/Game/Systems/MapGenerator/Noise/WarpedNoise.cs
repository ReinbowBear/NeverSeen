
public class WarpedNoise : INoise
{
    private INoise baseNoise;
    private INoise warpNoise;
    private float warpStrength;

    public WarpedNoise(INoise baseNoise, INoise warpNoise, float warpStrength)
    {
        this.baseNoise = baseNoise;
        this.warpNoise = warpNoise;
        this.warpStrength = warpStrength;
    }


    public float GetHeight(float x, float y)
    {
        float warpX = warpNoise.GetHeight(x + 1000, y + 1000);
        float warpY = warpNoise.GetHeight(x - 1000, y - 1000);

        float warpedX = x + (warpX - 0.5f) * warpStrength;
        float warpedY = y + (warpY - 0.5f) * warpStrength;

        return baseNoise.GetHeight(warpedX, warpedY);
    }
}
