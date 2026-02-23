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


    public void GenerateBiome(BiomeSO biomeData, BiomeRule biomeRule, List<TileData> startTiles)
    {
        for (int i = 0; i < startTiles.Count; i++)
        {
            var cellStrategy = biomeStrategys[biomeData.Type];
            var grownTiles = automaton.Grow(cellStrategy, startTiles, biomeRule.Size);

            for (int ii = 0; ii < grownTiles.Count; ii++)
            {
                var tile = grownTiles[i];

                tile.BiomeType = biomeData.Type;
                PrepareData.FreeTiles.Remove(tile);
            }
        }
    }
}
