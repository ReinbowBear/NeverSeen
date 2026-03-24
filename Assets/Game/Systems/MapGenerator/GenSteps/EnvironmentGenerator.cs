using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator
{
    public IList<BiomeAssetSO> assets;
    public Dictionary<BiomeAssetSO, INoise> biomesNoise = new();

    public void SetAssets(IList<BiomeAssetSO> assets)
    {
        this.assets = assets;
    }


    public void GenerateEnvironment(TileMap tileMap, int seed, INoise continentNoise)
    {
        foreach (var asset in assets)
        {
            var noise = asset.NoisePipeline.GetResult(seed, continentNoise);
            biomesNoise[asset] = noise;
        }

        foreach (var asset in assets)
        {
            foreach (var tile in tileMap.Tiles.Values)
            {
                var worldPos = tileMap.CubeToWorld(tile.CubeCoord, Tile.HexSize);

                if(biomesNoise[asset].GetHeight(worldPos.x, worldPos.y) < asset.ValueToSpawn) return;
                if(asset.Condition.GetScore(tile) < 15) return;

                tile.Takers.Push(asset.prefab);
            }
        }
    }
}
