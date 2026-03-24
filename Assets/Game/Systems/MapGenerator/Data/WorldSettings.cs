using UnityEngine;

[System.Serializable]
public class WorldSettings
{
    [Header("General")]
    public int Seed = 12345;
    public int MapRadius = 5;
    public float MaxHeight = 1f; // верхняя граница рельефа
    public float MinHeight = 0.2f;

    [Space]
    [Header("Continents")]
    public float ContinentScale = 5f;
    public int ContinentDetailLevel = 2;
    public float ContinentDetailStrength = 2f;

    [Space]
    [Header("Relief")]
    public float DetailScale = 1f;
    public int DetailLevel = 4;
    public float DetailStrength = 2f;

    [Space]
    [Header("Warp")]
    public float WarpScale = 0.02f;
    public float WarpStrength = 25f;

    [Space]
    [Header("Mask")]
    public float RadialMaskRadius = 20f;
    public float RadialMaskFalloff = 2f; // как резко обрывается мир

    [Space]
    [Header("Biomes")]
    public float MoistureScale = 0.5f;
    public int MoistureDetailLevel = 4;

    public float TemperatureScale = 0.5f;
    public int TemperatureDetailLevel = 4;

    [Space]
    [Header("WorldStyle")]
    public bool SingleContinent = true;      // один большой материк или несколько
    public bool SharpEdges = false;          // резкие переходы рельефа
    public bool SmoothTransitions = true;    // плавные переходы между биомами


    public void Randomize(System.Random rng)
    {
        Seed = rng.Next();
        MaxHeight = 0.6f + (float)rng.NextDouble() * 0.4f;

        ContinentScale = 0.002f + (float)rng.NextDouble() * 0.01f;
        DetailScale = 0.02f + (float)rng.NextDouble() * 0.05f;
        WarpStrength = 10f + (float)rng.NextDouble() * 40f;
        RadialMaskRadius = 200f + (float)rng.NextDouble() * 400f;
        RadialMaskFalloff = 1f + (float)rng.NextDouble() * 3f;
        SingleContinent = rng.NextDouble() > 0.5;
        SharpEdges = rng.NextDouble() > 0.5;
        SmoothTransitions = !SharpEdges;
    }
}
