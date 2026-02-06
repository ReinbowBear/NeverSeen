using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileMap
{
    public Dictionary<Vector3Int, Tile> Tiles = new();


    public bool IsCanPlace(Tile center, ShapeType shapeType)
    {
        foreach (var offset in Shape.Shapes[shapeType])
        {
            Vector3Int tilePos = center.tileData.CubeCoord + offset;

            if (Tiles.TryGetValue(tilePos, out Tile tileOnMap) == false) return false;

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

    public Vector3 CubeToWorld(Vector3Int cube, float width)
    {
        float x = width * (cube.x + cube.z / 2f); // без f в цифрах всё округляется вниз и позиционирование тайлов ломается
        float z = width * Mathf.Sqrt(3f) / 2f * cube.z;

        return new Vector3(x, 0, z);
    }
}
