using Unity.Mathematics;

public class Noise : INoise
{
    public float Scale;
    public int DetailLevel;
    public float DetailStrength; // сила влияния каждого следующего уровня деталей 0.5f = в двое слабее за предыдущий
    public float Fluctuations; // колебания, волнообразность шума

    private float2 offset; // сдвигает центр шума
    public float2 Offset => offset;

    public Noise(int seed, float scale = 10f, int detailLevel = 2, float detailStrength = 0.5f, float fluctuations = 2f)
    {
        Scale = scale;
        DetailLevel = detailLevel;
        DetailStrength = detailStrength;
        Fluctuations = fluctuations;

        Random rand = new Random((uint)seed);
        offset = rand.NextFloat2(-1000, 1000);
    }


    public float GetHeight(float x, float y)
    {
        float amplitude = 1f;
        float frequency = 1f;
        float noiseHeight = 0f;
        float maxValue = 0f;

        for (int i = 0; i < DetailLevel; i++)
        {
            float2 samplePos = new float2(x, y) * Scale * frequency + offset;

            float noiseValue = noise.snoise(samplePos); // -1 to 1
            noiseHeight += noiseValue * amplitude;

            maxValue += amplitude;

            amplitude *= DetailStrength;
            frequency *= Fluctuations;
        }

        noiseHeight /= maxValue;
        return (noiseHeight + 1f) * 0.5f; // Нормализуем из [-1,1] в [0,1]
    }
}

public interface INoise
{
    float GetHeight(float x, float y);
}
