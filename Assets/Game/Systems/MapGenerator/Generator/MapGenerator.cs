using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    private MyRandom random;

    private MapGenData mapGenData;
    private MapGenContext genContext;

    private TileGenerator mapTilesGen;
    private BiomeGenerator mapBiomes;
    private EnvironmentGenerator mapEnvironment;

    public MapGenerator(MapGenData mapGenData, MapGenContext genContext, MyRandom random)
    {
        this.mapGenData = mapGenData;
        this.genContext = genContext;
        this.random = random;

        mapTilesGen = new TileGenerator(genContext, random);
        mapBiomes = new BiomeGenerator(genContext, random);
        mapEnvironment = new EnvironmentGenerator(genContext, random);
    }


    public void GenerateMap()
    {
        mapTilesGen.CreateTiles(mapGenData);
        mapTilesGen.SetNeighbors();

        for (int i = 0; i < mapGenData.Biomes.Length; i++)
        {
            var tiles = genContext.FreeTiles; // не учитывает биом просто свободные тайлы
            var count = mapGenData.BiomesRule[i].Count;

            var biome = mapGenData.Biomes[i];
            var biomeRule = mapGenData.BiomesRule[i];

            var randomTiles = random.GetRandomElements(tiles, count);

            mapBiomes.GenerateBiome(biome, biomeRule, randomTiles);
        }


        for (int i = 0; i < mapGenData.EnvironmentsRule.Length; i++)
        {
            var tiles = genContext.FreeTiles; // не учитывает биом просто свободные тайлы

            var min = mapGenData.EnvironmentsRule[i].MinCount;
            var max = mapGenData.EnvironmentsRule[i].MaxCount;
            var EnvCount = random.System.Next(min, max);

            var randomTiles = random.GetRandomElements(tiles, EnvCount);

            mapEnvironment.Generate(new GameObject(), mapGenData.EnvironmentsRule[i], EnvCount);
        }
    }
}

[System.Serializable]
public class MapGenData
{
    public int Radius;
    public BiomeSO[] Biomes;
    public BiomeRule[] BiomesRule;
    public EnvironmentRule[] EnvironmentsRule;
}

public class MapGenContext
{
    public Dictionary<Vector3Int, TileData> TilesData;
    public List<TileData> FreeTiles;

    public MapGenContext()
    {
        TilesData = new();
        FreeTiles = new();
    }
}
