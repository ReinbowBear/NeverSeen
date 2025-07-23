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
        float tintRange = 0.05f;
        renderMat.material.color += new Color(Random.Range(-tintRange, tintRange), Random.Range(-tintRange, tintRange), Random.Range(-tintRange, tintRange));
    }


    public void ActiveTile()
    {
        highlight.enabled = true;
    }

    public void DeactivateTile()
    {
        highlight.enabled = false;
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
    public Vector3Int cubeCoord;
    public GameObject isTaken;

    public TileType tileType = TileType.ground;
    public List<TileData> neighbors = new();

    public TileData(Vector3Int coord)
    {
        cubeCoord = coord;
    }
}

public enum TileType
{
    bottom, ground, hill, mount
}
