using System.Collections.Generic;
using UnityEngine;

public class ProxyMapGenerator : BaseProxy
{
    public Transform MapRoot;

    public TileMap MapData = new();
    public MyRandom Random = new();

    public MapGenData MapGenData = new();
    public MapDefinitions MapDefinition = new();
    public MapGenContext GenContext = new();

    private TileGenerator mapTilesGen;
    private BiomeGenerator mapBiomes;
    private EnvironmentGenerator mapEnvironment;

    public override void Init()
    {
        mapTilesGen = new TileGenerator(GenContext, Random);
        mapBiomes = new BiomeGenerator(GenContext, Random);
        mapEnvironment = new EnvironmentGenerator(GenContext, Random);
    }

    public override void Enter()
    {
        eventWorld.AddListener(GenerateMap, Events.SceneEvents.EnterScene);
    }

    public override void Exit()
    {
        eventWorld.RemoveListener(GenerateMap, Events.SceneEvents.EnterScene);
    }


    private void GenerateMap()
    {
        mapTilesGen.CreateTiles(MapGenData);
        mapTilesGen.SetNeighbors();

        for (int i = 0; i < MapGenData.BiomeRules.Count; i++)
        {
            var tiles = GenContext.FreeTiles;
            var biomeRule = MapGenData.BiomeRules[i];
            var biome = MapDefinition.Biomes[i];

            List<TileData> randomTiles = Random.GetRandomElements(tiles, biomeRule.Count); // раньше элементы выбирались в генерации биомов и забирались из списка свободных! не помню исправил ли
            mapBiomes.GenerateBiome(biome, biomeRule, randomTiles);
        }

        // mapEnvironment.Generate(environmentGenData);
    }
}

[System.Serializable]
public class MapGenData
{
    public int Radius;
    public List<BiomeRule> BiomeRules;
    public List<EnvironmentRule> EnvironmentRules;
}

public class MapDefinitions
{
    public List<BiomeData> Biomes;
    public List<EnvironmentData> Environments;
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
