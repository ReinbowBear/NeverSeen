using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Entity
{
    [SerializeField] protected ResourceType resourceType;
    [SerializeField] protected short value;
    [Space]
    [SerializeField] protected Vector3Int[] Shape;
    private Tile tile;

    protected void Awake()
    {
        Shape = new Vector3Int[]
        {
            new Vector3Int( 1, -1,  0),
            new Vector3Int( 1,  0, -1),
            new Vector3Int( 0,  1, -1),

            new Vector3Int(-1,  1,  0),
            new Vector3Int(-1,  0,  1),
            new Vector3Int( 0, -1,  1)
        };
    }


    public void Active()
    {
        foreach (var offset in Shape)
        {
            Vector3Int tilePos = tile.tileData.cubeCoord + offset;

            var Tile = MapGenerator.Instance.TileMap[tilePos];
            Tile.ActiveTile();
        }

        PlayerResource.Instance.AddBuilding(this);
    }

    public void Work(Dictionary<ResourceType, int> resourceDictionary)
    {
        resourceDictionary[resourceType] += value;
    }

    public void Deactive()
    {
        PlayerResource.Instance.RemoveBuilding(this);
    }


    public void OnSelected()
    {

    }

    public void Unselected()
    {

    }


    public void Delete()
    {
        Deactive();
        Destroy(gameObject);
    }

    #region Spawn
    public IEnumerator OnSpawn()
    {
        Camera cam = Camera.main;
        LayerMask rayLayer = LayerMask.GetMask("Ground");

        while (true) // стрёмновато так!
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 30, rayLayer))
            {
                transform.position = hit.transform.position;
            }

            yield return null;
        }
    }

    public bool TryPlace(Tile newTile)
    {
        foreach (var offset in Shape)
        {
            Vector3Int tilePos = newTile.tileData.cubeCoord + offset;
            if (MapGenerator.Instance.TileMap.TryGetValue(tilePos, out Tile tile) == false)
            {
                return false;
            }

            if (tile.tileData.isTaken != null)
            {
                return false;
            }

            if (tile.tileData.tileType != newTile.tileData.tileType)
            {
                return false;
            }
        }

        StopAllCoroutines();
        newTile.tileData.isTaken = gameObject;
        tile = newTile;

        Active();
        return true;
    }
    #endregion

    #region Utility
    public List<Tile> GetTilesInRadius(Vector3Int center, int radius)
    {
        List<Tile> result = new();

        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = Mathf.Max(-radius, -dx - radius); dy <= Mathf.Min(radius, -dx + radius); dy++)
            {
                int dz = -dx - dy;

                Vector3Int offset = new Vector3Int(dx, dy, dz);
                Vector3Int neighborCoord = center + offset;

                if (MapGenerator.Instance.TileMap.TryGetValue(neighborCoord, out Tile tile))
                {
                    result.Add(tile);
                }
            }
        }
        return result;
    }
    #endregion
}
