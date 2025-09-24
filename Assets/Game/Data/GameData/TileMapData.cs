using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileMapData
{
    public Dictionary<Vector3Int, Tile> TileMap = new();
    public List<Entity> Buildings = new();
    public Entity CurrentEntity;

    public bool CanPlace(Tile center, ShapeType shapeType)
    {
        foreach (var offset in Shape.Shapes[shapeType])
        {
            Vector3Int tilePos = center.tileData.CubeCoord + offset;

            if (TileMap.TryGetValue(tilePos, out Tile tileOnMap) == false) return false;

            if (tileOnMap.tileData.IsTaken != null) return false;

            if (tileOnMap.tileData.TileHeightType != center.tileData.TileHeightType) return false;
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

                if (TileMap.TryGetValue(neighborCoord, out Tile newTile))
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
}
