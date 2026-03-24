using UnityEngine;

public class TileGenerator
{
    public Vector3Int[] CubeCords = new Vector3Int[]
    {
        new Vector3Int( 1, -1,  0),
        new Vector3Int( 1,  0, -1),
        new Vector3Int( 0,  1, -1),

        new Vector3Int(-1,  1,  0),
        new Vector3Int(-1,  0,  1),
        new Vector3Int( 0, -1,  1)
    };


    public void CreateTilesData(TileMap tileMap, int radius)
    {
        for (int i = -radius; i <= radius; i++)
        {
            int max = Mathf.Max(-radius, -i - radius);
            int min = Mathf.Min(radius, -i + radius);

            for (int ii = max; ii <= min; ii++)
            {
                int y = -i - ii; // третья нулевая координата у нас это "Y" высота

                Vector3Int coord = new(i, y, ii);
                Tile tile = new Tile(coord);

                tileMap.Tiles.Add(coord, tile);
            }
        }
    }

    public void SetNeighbors(TileMap tileMap)
    {
        foreach (var tile in tileMap.Tiles.Values)
        {
            foreach (var dir in CubeCords)
            {
                var neighborCoord = tile.CubeCoord + dir;
                if (!tileMap.Tiles.TryGetValue(neighborCoord, out var neighbor)) continue;

                tile.Neighbors.Add(neighbor);
            }
        }
    }
}
