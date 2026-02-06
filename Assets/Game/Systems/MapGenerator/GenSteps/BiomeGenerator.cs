using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator
{
    public Dictionary<BiomeType, ICell> biomeStrategys = new();
    public MapGenContext PrepareData;
    public MyRandom Random;

    private CellularAutomaton automaton = new();

    public BiomeGenerator(MapGenContext prepareData, MyRandom random)
    {
        PrepareData = prepareData;
        Random = random;

        biomeStrategys.Add(BiomeType.Ground, new DefaultCell());
    }


    public void GenerateBiome(BiomeData biomeData, BiomeRule biomeRule, List<TileData> startTiles)
    {
        foreach (var startTile in startTiles)
        {
            var cellStrategy = biomeStrategys[biomeData.BiomeType];
            var grownTiles = automaton.Grow(cellStrategy, startTiles, biomeRule.Size);

            foreach (var tile in grownTiles)
            {
                tile.TileHeightType = biomeData.BiomeType;
                PrepareData.FreeTiles.Remove(tile);
            }
        }
    }
}


[System.Serializable]
public struct BiomeRule
{
    public int Count;
    public int Size;
}

[System.Serializable]
public struct BiomeData
{
    public BiomeType BiomeType;
    public Color Color;
}


public enum BiomeType
{
    Bottom, 
    Ground, 
    Hill, 
    Mount
}
