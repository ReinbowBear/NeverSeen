using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform mapPoint;

    [SerializeField] private float hexWidth; // диамтр от плоской стороны к плоской, при диаметре 1m это будет 0.866
    [SerializeField] private float hexY;
    [Space]
    [SerializeField] private int mapRadius;

    [Header("Ores")]
    [SerializeField] private byte oresTiles;

    [Header("Hills")]
    [SerializeField] private byte hillCount;
    [SerializeField] private byte hillTiles;

    [Header("Mountains")]
    [SerializeField] private byte mountCount;
    [SerializeField] private byte mountTiles;

    private Dictionary<Vector3Int, TileData> tilesData; // Vector3Int нужен для установки соседей, да и в целом весь список для генерации данных карты
    private List<TileData> freeTiles;

    [Inject] private Factory factory;
    [Inject] private TileMapData mapData;
    [Inject] private MyRandom random;


    [EventHandler(Priority.low)]
    private void GenerateMap(OnSceneStart _)
    {
        tilesData = new();
        freeTiles = new();

        CreateTiles();
        SetNeighbors();

        var hillCuts = GetMountTilesCount(hillCount, hillTiles);
        var mountCuts = GetMountTilesCount(mountCount, mountTiles);

        SetTileType(freeTiles, oresTiles, TileType.ore);

        foreach (var count in hillCuts)
        {
            int randomTile = random.System.Next(0, freeTiles.Count);
            SetTileHeightType(freeTiles[randomTile], count, TileHeightType.hill);
        }

        foreach (var count in mountCuts)
        {
            int randomTile = random.System.Next(0, freeTiles.Count);
            SetTileHeightType(freeTiles[randomTile], count, TileHeightType.mount);
        }

        StartCoroutine(DisplayMap());
    }

    #region DisplayMap
    private IEnumerator DisplayMap()
    {
        var handle = factory.GetAsset("Tile");
        yield return new WaitUntil(() => handle.IsCompleted);

        foreach (var tileData in tilesData.Values)
        {
            Vector3 pos = CubeToWorld(tileData.CubeCoord, hexWidth);
            pos.y = (int)tileData.TileHeightType * hexY;

            GameObject obj = Instantiate(handle.Result);
            obj.transform.position = pos;
            obj.transform.SetParent(mapPoint);

            Tile component = obj.GetComponent<Tile>();
            component.tileData = tileData;

            mapData.TileMap.Add(tileData.CubeCoord, component);
        }
        tilesData = null;
        freeTiles = null;
    }
    #endregion

    #region Generate
    private void CreateTiles()
    {
        for (int x = -mapRadius; x <= mapRadius; x++)
        {
            for (int y = Mathf.Max(-mapRadius, -x - mapRadius); y <= Mathf.Min(mapRadius, -x + mapRadius); y++)
            {
                int z = -x - y;
                Vector3Int coord = new(x, y, z);

                TileData tile = new TileData(coord);
                tilesData.Add(coord, tile);
                freeTiles.Add(tile);
            }
        }
    }

    private void CreateCubeTiles(int width, int height) // функция для квадратной карты
    {
        for (int row = 0; row < height; row++)
        {
            int rowOffset = Mathf.FloorToInt(row / 2f); // Смещение для "pointy-top"
            for (int col = -rowOffset; col < width - rowOffset; col++)
            {
                int x = col;
                int z = row;
                int y = -x - z;

                Vector3Int cubeCoord = new Vector3Int(x, y, z);
                TileData tile = new TileData(cubeCoord);
                tilesData.Add(cubeCoord, tile);
                freeTiles.Add(tile);
            }
        }
    }
    #endregion

    #region SetTilesData
    private void SetNeighbors()
    {
        Vector3Int[] cubeCords = new Vector3Int[]
        {
            new Vector3Int( 1, -1,  0),
            new Vector3Int( 1,  0, -1),
            new Vector3Int( 0,  1, -1),

            new Vector3Int(-1,  1,  0),
            new Vector3Int(-1,  0,  1),
            new Vector3Int( 0, -1,  1)
        };

        foreach (var tile in tilesData.Values)
        {
            foreach (var dir in cubeCords)
            {
                Vector3Int neighborCoord = tile.CubeCoord + dir;
                if (tilesData.TryGetValue(neighborCoord, out var neighbor))
                {
                    if (neighbor.TileHeightType != tile.TileHeightType) continue;
                    tile.Neighbors.Add(neighbor);
                }
            }
        }
    }


    private List<int> GetMountTilesCount(int cutsCount, int totalTiles)
    {
        List<int> cuts = new();
        HashSet<int> uniqueCuts = new();

        while (uniqueCuts.Count < cutsCount - 1)
        {
            uniqueCuts.Add(random.System.Next(1, totalTiles));
        }

        cuts.Add(0);
        cuts.AddRange(uniqueCuts);
        cuts.Add(totalTiles);
        cuts.Sort();

        List<int> result = new();
        for (int i = 0; i < cutsCount; i++)
        {
            result.Add(cuts[i + 1] - cuts[i]);
        }

        return result;
    }


    private void SetTileType(List<TileData> tiles, int size, TileType type)
    {
        HashSet<TileData> visited = new();

        int count = 0;
        while (count < size && visited.Count < tiles.Count)
        {
            int randomIndex = random.System.Next(0, tiles.Count);
            TileData tileData = tiles[randomIndex];

            if (visited.Contains(tileData)) continue;

            tileData.TileType = type;
            visited.Add(tileData);
            count++;
        }
    }

    private void SetTileHeightType(TileData startTile, int size, TileHeightType type)
    {
        Queue<TileData> stack = new();
        HashSet<TileData> visited = new();

        stack.Enqueue(startTile);
        visited.Add(startTile);

        startTile.TileHeightType = type;
        freeTiles.Remove(startTile);

        int count = 1;
        while (stack.Count > 0 && count < size)
        {
            var current = stack.Dequeue();
            foreach (var neighbor in current.Neighbors)
            {
                if (!visited.Contains(neighbor) && neighbor.TileHeightType == TileHeightType.ground)
                {
                    neighbor.TileHeightType = type;

                    stack.Enqueue(neighbor);
                    visited.Add(neighbor);
                    freeTiles.Remove(neighbor);

                    count++;
                    if (count >= size) break;
                }
            }
        }
    }
    #endregion

    #region Other
    private Vector3 CubeToWorld(Vector3Int cube, float width)
    {
        float x = width * (cube.x + cube.z / 2f); // без f в цифрах всё округляется вниз и позиционирование тайлов ломается
        float z = width * Mathf.Sqrt(3f) / 2f * cube.z;

        return new Vector3(x, 0, z);
    }
    #endregion
}
