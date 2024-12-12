using System;
using UnityEngine;

public class GlobalMap : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float fillPercentage;
    [SerializeField] private int smoothingIterations;

    private int[,] map;

    public System.Random random;
    private int seed;

    void Awake()
    {
        seed = DateTime.Now.Millisecond;
        random = new System.Random(seed);
    }


    private void GenerateMap()
    {
        map = new int[width, height];

        for (byte x = 0; x < width; x++)
        {
            for (byte y = 0; y < height; y++)
            {
                map[x, y] = UnityEngine.Random.value < fillPercentage? 1 : 0; // 1 - земля, 0 - пустота
            }
        }

        for (byte i = 0; i < smoothingIterations; i++)
        {
            SmoothMap();
        }
    }

    private void SmoothMap()
    {
        int[,] newMap = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWalls = GetNeighbourWallCount(x, y);

                if (neighbourWalls > 4)
                {
                    newMap[x, y] = 1; // Земля
                }
                else if (neighbourWalls < 4)
                {
                    newMap[x, y] = 0; // Пустота
                }
            }
        }

        map = newMap;
    }

    private int GetNeighbourWallCount(int x, int y)
    {
        int wallCount = 0;
        for (int neighbourX = -1; neighbourX <= 1; neighbourX++)
        {
            for (int neighbourY = -1; neighbourY <= 1; neighbourY++)
            {
                int checkX = x + neighbourX;
                int checkY = y + neighbourY;

                if (neighbourX == 0 && neighbourY == 0)
                {
                    continue;
                }

                if (checkX < 0 || checkX >= width || checkY < 0 || checkY >= height) //если соседний тайл выходит за пределы карты, считаем его как стену
                {
                    wallCount++;
                }
                else
                {
                    wallCount += map[checkX, checkY];
                }
            }
        }

        return wallCount;
    }

    private void DisplayMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tile = tilePrefabs[map[x, y]];
                Instantiate(tile, new Vector3(x, 0, y), Quaternion.identity);
            }
        }
    }


    private void Save()
    {
        SaveGlobalMap saveGlobalMap = new SaveGlobalMap();
        saveGlobalMap.seed = seed;

        SaveSystem.gameData.saveGlobalMap = saveGlobalMap;
    }

    private void Load()
    {
        SaveGlobalMap saveGlobalMap = SaveSystem.gameData.saveGlobalMap;
        seed = saveGlobalMap.seed;

        random = new System.Random(seed);
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryMap>(GenerateMap, 1);
        EventBus.Add<MyEvent.OnEntryMap>(DisplayMap);

        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
    }

    void OnDisable()
    {
        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
    }
}

[System.Serializable]
public struct SaveGlobalMap
{
    public int seed;
}
