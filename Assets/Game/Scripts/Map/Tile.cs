using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private MeshRenderer renderMat;
    [SerializeField] private MeshRenderer highlight;

    [HideInInspector] public int toStartCost;
    [HideInInspector] public int toTargetCost;
    [HideInInspector] public int fullCost => toStartCost + toTargetCost;
    [HideInInspector] public Tile parent;

    [HideInInspector] public TileData tileData;

    void Awake()
    {
        float tintRange = 0.1f;
        renderMat.material.color += new Color(Random.Range(-tintRange, tintRange), Random.Range(-tintRange, tintRange), Random.Range(-tintRange, tintRange));
    }


    public void ActiveTile(bool isActive)
    {
        highlight.enabled = isActive;
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


public class TileData
{
    public Vector3Int CubeCoord;
    public Entity IsTaken;

    public TileType TileType = TileType.empty;
    public TileHeightType TileHeightType = TileHeightType.ground;

    public List<TileData> Neighbors = new();

    public TileData(Vector3Int cord)
    {
        CubeCoord = cord;
    }
}


public enum TileType
{
    empty, ore,
}

public enum TileHeightType
{
    bottom, ground, hill, mount
}
