using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public class MapGenerator
{
    private TileMap tileMap;
    private Factory factory;
    private RandomService random;

    private TileGenerator tilesGen;
    private BiomeGenerator biomesGen;
    private EnvironmentGenerator environmentGen;

    public WorldSettings Settings = new();

    public MapGenerator(TileMap tileMap, Factory factory, RandomService random)
    {
        this.tileMap = tileMap;
        this.factory = factory;
        this.random = random;

        tilesGen = new TileGenerator();
        biomesGen = new BiomeGenerator();
        environmentGen = new EnvironmentGenerator();
    }


    public async Task GenerateNewMap()
    {
        tilesGen.CreateTilesData(tileMap, Settings.MapRadius);
        tilesGen.SetNeighbors(tileMap);

        var biomes = await factory.LoadByLabelAsync<BiomeSO>("Biome");
        var assets = await factory.LoadByLabelAsync<BiomeAssetSO>("BiomeAsset");

        var worldNoise = new WorldNoise(Settings);
        worldNoise.SetData(tileMap);

        biomesGen.SetGenBiomes(biomes);
        biomesGen.GenerateBiomes(tileMap, Settings.Seed, worldNoise.ContinentNoise);

        environmentGen.SetAssets(assets);
        environmentGen.GenerateEnvironment(tileMap, Settings.Seed, worldNoise.ContinentNoise);
    }
}



public class IslandGenerator
{
    public static float[,] GenerateIslandMap(int width, int height, int seed)
    {
        float[,] map = new float[width, height];

        Noise baseNoise = new Noise(seed, 0.01f, 4, 0.5f, 2f);
        Noise warpNoise = new Noise(seed + 1, 0.005f, 2, 0.8f, 2f);

        WarpedNoise warpedBase = new WarpedNoise(baseNoise, warpNoise, warpStrength: 50f);
        MaskNoise islandMask = new MaskNoise(warpedBase, baseNoise.Offset, width / 2f, 2f);

        Noise lakeNoise = new Noise(seed + 2, 0.02f, 3, 0.5f, 2f);
        MaskNoise lakeMask = new MaskNoise(lakeNoise, lakeNoise.Offset, width / 4f, 3f);

        Noise ridgeNoise = new Noise(seed + 3, 0.005f, 5, 0.7f, 2f);
        MaskNoise ridgeMask = new MaskNoise(ridgeNoise, width / 2f, 1.5f);

        MaskNoise beachMask = new MaskNoise(new Noise(seed + 4, 0.01f, 1, 0.5f, 1f), width / 2f, 1.2f);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float coordX = x - width / 2f;
                float coordY = y - height / 2f;

                float islandHeight = islandMask.GetHeight(coordX, coordY);
                float beachHeight = islandHeight * beachMask.GetHeight(coordX, coordY);
                float ridgeHeight = ridgeMask.GetHeight(coordX, coordY) * ridgeNoise.GetHeight(coordX, coordY);

                float lakeValue = lakeMask.GetHeight(coordX, coordY);
                float finalHeight = Mathf.Lerp(beachHeight + ridgeHeight, 0f, lakeValue);

                float snowThreshold = 0.8f;
                if (finalHeight > snowThreshold)
                {
                    float snowAmount = (finalHeight - snowThreshold) / (1 - snowThreshold);
                    finalHeight = Mathf.Lerp(finalHeight, 1f, snowAmount);
                }

                map[x, y] = Mathf.Clamp01(finalHeight);
            }
        }

        return map;
    }


    public static float[,] GenerateIslandMapFromBiomes(int width, int height, int seed, int islandCount = 3)
    {
        float[,] map = new float[width, height];
    
        System.Random rand = new(seed);
    
        for (int i = 0; i < islandCount; i++)
        {
            float centerX = (float)rand.NextDouble() * width;
            float centerY = (float)rand.NextDouble() * height;
    
            float islandRadius = 50f + (float)rand.NextDouble() * 50f;
    
            Noise baseNoise = new Noise(seed + i * 10, 0.01f, 4, 0.5f, 2f);
            Noise warpNoise = new Noise(seed + i * 10 + 1, 0.005f, 2, 0.8f, 2f);
            WarpedNoise warpedBase = new WarpedNoise(baseNoise, warpNoise, 50f);
    
            MaskNoise islandMask = new MaskNoise(warpedBase, new float2(centerX, centerY), islandRadius, 2f);
            MaskNoise beachMask = new MaskNoise(new Noise(seed + i * 10 + 4, 0.01f, 1, 0.5f, 1f), new float2(centerX, centerY), islandRadius, 1.2f);
    
            Noise lakeNoise = new Noise(seed + i * 10 + 2, 0.02f, 3, 0.5f, 2f);
            MaskNoise lakeMask = new MaskNoise(lakeNoise, new float2(centerX, centerY), islandRadius / 2f, 3f);
    
            Noise ridgeNoise = new Noise(seed + i * 10 + 3, 0.005f, 5, 0.7f, 2f);
            MaskNoise ridgeMask = new MaskNoise(ridgeNoise, new float2(centerX, centerY), islandRadius, 1.5f);
    
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float coordX = x;
                    float coordY = y;
    
                    float islandHeight = islandMask.GetHeight(coordX, coordY);
                    float beachHeight = islandHeight * beachMask.GetHeight(coordX, coordY);
                    float ridgeHeight = ridgeMask.GetHeight(coordX, coordY) * ridgeNoise.GetHeight(coordX, coordY);
    
                    float lakeValue = lakeMask.GetHeight(coordX, coordY);
                    float finalHeight = Mathf.Lerp(beachHeight + ridgeHeight, 0f, lakeValue);
    
                    float snowThreshold = 0.8f;
                    if (finalHeight > snowThreshold)
                    {
                        float snowAmount = (finalHeight - snowThreshold) / (1 - snowThreshold);
                        finalHeight = Mathf.Lerp(finalHeight, 1f, snowAmount);
                    }
    
                    map[x, y] = Mathf.Clamp01(map[x, y] + finalHeight);
                }
            }
        }
    
        return map;
    }
}
