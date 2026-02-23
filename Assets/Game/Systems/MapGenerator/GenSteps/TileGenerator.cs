using UnityEngine;

public class TileGenerator
{
    public MapGenContext PrepareData;
    public MyRandom Random;

    public Vector3Int[] cubeCords = new Vector3Int[]
    {
        new Vector3Int( 1, -1,  0),
        new Vector3Int( 1,  0, -1),
        new Vector3Int( 0,  1, -1),

        new Vector3Int(-1,  1,  0),
        new Vector3Int(-1,  0,  1),
        new Vector3Int( 0, -1,  1)
    };

    public TileGenerator(MapGenContext prepareData, MyRandom random)
    {
        PrepareData = prepareData;
        Random = random;
    }


    public void CreateTiles(MapGenData genData)
    {
        var mapRadius = genData.Radius;

        for (int i = -mapRadius; i <= mapRadius; i++)
        {
            int max = Mathf.Max(-mapRadius, -i - mapRadius);
            int min = Mathf.Min(mapRadius, -i + mapRadius);

            for (int ii = max; ii <= min; ii++)
            {
                int y = -i - ii; // третья нулевая координата у нас это "Y" высота

                Vector3Int coord = new(i, y, ii);
                TileData tile = new TileData(coord);

                PrepareData.TilesData.Add(coord, tile);
                PrepareData.FreeTiles.Add(tile);
            }
        }
    }

    public void SetNeighbors()
    {
        foreach (var tile in PrepareData.TilesData.Values)
        {
            foreach (var dir in cubeCords)
            {
                var neighborCoord = tile.CubeCoord + dir;
                if (!PrepareData.TilesData.TryGetValue(neighborCoord, out var neighbor)) continue;

                tile.Neighbors.Add(neighbor);
            }
        }
    }
}
