using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator
{
    public IList<BiomeSO> biomes;
    public Dictionary<BiomeSO, INoise> biomesNoise = new();

    public void SetGenBiomes(IList<BiomeSO> biomes)
    {
        this.biomes = biomes;
    }


    public void GenerateBiomes(TileMap tileMap, int seed, INoise continentNoise)
    {
        foreach (var biome in biomes)
        {
            var noise = biome.NoisePipeline.GetResult(seed, continentNoise);
            biomesNoise[biome] = noise;
        }


        foreach (var tile in tileMap.Tiles.Values)
        {
            tile.Biome = ResolveBiome(tile);

            var worldPos = tileMap.CubeToWorld(tile.CubeCoord, Tile.HexSize);
            var noise = biomesNoise[tile.Biome];

            tile.Height = noise.GetHeight(worldPos.x, worldPos.y);
        }
    }

    private BiomeSO ResolveBiome(Tile tile)
    {
        BiomeSO best = null;
        float bestScore = 0;

        foreach (var biome in biomes)
        {
            float score = biome.Condition.GetScore(tile);

            if (score > bestScore)
            {
                bestScore = score;
                best = biome;
            }
        }

        return best;
    }
}
