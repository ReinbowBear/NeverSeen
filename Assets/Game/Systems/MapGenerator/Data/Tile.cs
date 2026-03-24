using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public const float HexSize = 1f; // от центра до вершины

    public Vector3Int CubeCoord;
    public BiomeSO Biome;
    public Region Region;

    public float Height;
    public float Moisture;
    public float Temperature;

    public Stack<GameObject> Takers = new();
    public List<Tile> Neighbors = new();

    public GameObject IsTaken => Takers.Count > 0 ? Takers.Peek() : null;
    public BiomeType BiomeType => Biome.Type;

    public Tile(Vector3Int cord, BiomeSO biome = null, Region region = null)
    {
        CubeCoord = cord;
        Biome = biome;
        Region = region;
    }
}
