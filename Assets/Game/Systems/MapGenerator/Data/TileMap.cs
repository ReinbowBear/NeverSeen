using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileMap
{
    public Dictionary<Vector3Int, Tile> Tiles = new();


    public bool IsCanPlace(Tile tile, ShapeType shapeType)
    {
        foreach (var offset in Shape.Shapes[shapeType])
        {
            Vector3Int tilePos = tile.CubeCoord + offset;

            if (Tiles.TryGetValue(tilePos, out Tile tileOnMap) == false) return false;

            if (tileOnMap.IsTaken != null) return false;

            if (tileOnMap.BiomeType != tile.BiomeType) return false;
        }
        return true;
    }


    public List<Tile> GetTilesInRadius(Vector3Int center, int radius)
    {
        var result = new List<Tile>(3 * radius * (radius + 1));

        for (int cubeX = -radius; cubeX <= radius; cubeX++)
        {
            for (int cubeY = Mathf.Max(-radius, -cubeX - radius); cubeY <= Mathf.Min(radius, -cubeX + radius); cubeY++)
            {
                if (cubeX == 0 && cubeY == 0) continue; // Пропустить центр

                int cubeZ = -cubeX - cubeY;
                Vector3Int offset = new Vector3Int(cubeX, cubeY, cubeZ);
                Vector3Int neighborCoord = center + offset;

                if (Tiles.TryGetValue(neighborCoord, out Tile newTile))
                {
                    result.Add(newTile);
                }
            }
        }
        return result;
    }


    public Tile GetTileFromCord(Vector3 pos)
    {
        if (Physics.Raycast(pos + Vector3.up * 10, Vector3.down, out RaycastHit hit, 20, LayerMask.GetMask("Tile")))
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            return tile;
        }
        return null;
    }

    public Vector3 CubeToWorld(Vector3Int cube, float hexSize) // pointy-top
    {
        float x = hexSize * Mathf.Sqrt(3f) * (cube.x + cube.z * 0.5f);
        float z = hexSize * 1.5f * cube.z;

        return new Vector3(x, 0f, z);
    }


    public int GetHexDistance(Transform transform) 
    {
        Vector3Int dist = new Vector3Int(Mathf.Abs((int)transform.position.x - (int)transform.position.x), Mathf.Abs((int)transform.position.z - (int)transform.position.z));

        int lowest = Mathf.Min(dist.x, dist.z);
        int highest = Mathf.Max(dist.x, dist.z);

        int horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10 ;
    }
}
