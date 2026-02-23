using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public MeshRenderer Highlight;
    public TileData TileData;


    public void SetBacklight(bool isActive)
    {
        Highlight.enabled = isActive;
    }

    public int GetDistance(Tile target) 
    {
        Vector3Int dist = new Vector3Int(Mathf.Abs((int)transform.position.x - (int)target.transform.position.x), Mathf.Abs((int)transform.position.z - (int)target.transform.position.z));

        int lowest = Mathf.Min(dist.x, dist.z);
        int highest = Mathf.Max(dist.x, dist.z);

        int horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10 ;
    }
}


public struct TileData
{
    public Vector3Int CubeCoord;
    public BiomeType BiomeType;

    public Stack<GameObject> Takers;
    public List<TileData> Neighbors;

    public GameObject IsTaken => Takers.Count > 0 ? Takers.Peek() : null;

    public TileData(Vector3Int cord, BiomeType biomeType = BiomeType.Bottom)
    {
        CubeCoord = cord;
        BiomeType = biomeType;

        Takers = new();
        Neighbors = new();
    }
}
