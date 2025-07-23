using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance;

    [SerializeField] private AssetReference tilePrefabs;
    [SerializeField] private float hexWidth; // диамтр от плоской стороны к плоской, при диаметре 1m это будет 0.866
    [SerializeField] private float hexY;

    [Space]
    [SerializeField] private Transform mapPoint;
    public int mapRadius;

    [Header("Hills")]
    [SerializeField] private byte hillCount;
    [SerializeField] private byte hillTiles;

    [Header("Mountains")]
    [SerializeField] private byte mountCount;
    [SerializeField] private byte mountTiles;

    public Dictionary<Vector3Int, Tile> TileMap;
    private Dictionary<Vector3Int, TileData> tilesData;
    private List<TileData> freeTiles;


    void Awake()
    {
        Instance = this;
        GenerateMap();
    }


    private void GenerateMap()
    {
        TileMap = new();
        tilesData = new();
        freeTiles = new();

        List<TileData> tileDatas = new();

        CreateTiles();
        SetNeighbors();

        var hillCuts = GetMountTilesCount(hillCount, hillTiles);
        var mountCuts = GetMountTilesCount(mountCount, mountTiles);

        foreach (var count in hillCuts)
        {
            int randomTile = MyRandom.random.Next(0, freeTiles.Count);
            SetTileType(freeTiles[randomTile], count, TileType.hill);
        }

        foreach (var count in mountCuts)
        {
            int randomTile = MyRandom.random.Next(0, freeTiles.Count);
            SetTileType(freeTiles[randomTile], count, TileType.mount);
        }

        StartCoroutine(DisplayMap());
    }

    #region DisplayMap
    private IEnumerator DisplayMap()
    {
        var tile = Loader.LoadAssetAsync<GameObject>(tilePrefabs.RuntimeKey.ToString());
        yield return new WaitUntil(() => tile.IsCompleted);

        foreach (var tileData in tilesData.Values)
        {
            Vector3 pos = CubeToWorld(tileData.cubeCoord, hexWidth);
            pos.y = (int)tileData.tileType * hexY;

            GameObject obj = Instantiate(tile.Result, pos, Quaternion.identity, mapPoint);
            Tile component = obj.GetComponent<Tile>();
            component.tileData = tileData;

            TileMap.Add(tileData.cubeCoord, component);
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

    private void CreateCubeTiles(int width, int height)
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
                Vector3Int neighborCoord = tile.cubeCoord + dir;
                if (tilesData.TryGetValue(neighborCoord, out var neighbor))
                {
                    if (neighbor.tileType != tile.tileType) continue;
                    tile.neighbors.Add(neighbor);
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
            uniqueCuts.Add(MyRandom.random.Next(1, totalTiles));
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

    private void SetTileType(TileData startTile, int size, TileType type)
    {
        Queue<TileData> queue = new();
        HashSet<TileData> visited = new();

        queue.Enqueue(startTile);
        visited.Add(startTile);
        startTile.tileType = type;
        freeTiles.Remove(startTile);

        int count = 1;
        while (queue.Count > 0 && count < size)
        {
            var current = queue.Dequeue();
            foreach (var neighbor in current.neighbors)
            {
                if (!visited.Contains(neighbor) && neighbor.tileType == TileType.ground)
                {
                    neighbor.tileType = type;

                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    freeTiles.Remove(neighbor);

                    count++;
                    if (count >= size) break;
                }
            }
        }
    }

    private Vector3 CubeToWorld(Vector3Int cube, float width)
    {
        float x = width * (cube.x + cube.z / 2f); // без f в цифрах всё округляется вниз и позиционирование тайлов ломается
        float z = width * Mathf.Sqrt(3f) / 2f * cube.z;

        return new Vector3(x, 0, z);
    }
    #endregion

    #region Other
    private void Save(OnSave _)
    {

    }

    private void Load(OnLoad _)
    {

    }


    void OnEnable()
    {
        EventBus.Add<OnSave>(Save);
        EventBus.Add<OnLoad>(Load);
    }

    void OnDisable()
    {
        EventBus.Remove<OnSave>(Save);
        EventBus.Remove<OnLoad>(Load);
    }
    #endregion
}
