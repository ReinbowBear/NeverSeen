using System.Collections.Generic;
using UnityEngine;

public static class MapFactory
{
    public static MapData mapData;

    private static byte wavesCount = 5;
    private static byte minEnemy = 3;
    private static byte maxEnemy = 6;

    public static void GenerateMap()
    {
        MapData newMap = new MapData();
        AddressImporter.ImportAddressable();
        List<string> maps = AddressImporter.addressStorage["Maps"];
        List<string> enemys = AddressImporter.addressStorage["Enemys"];

        int mapIndex = MyRandom.random.Next(0, maps.Count);
        newMap.mapModel = maps[mapIndex];
        newMap.wavesCount = wavesCount;

        for (int i = 0; i < wavesCount; i++)
        {
            int enemysCount = MyRandom.random.Next(minEnemy, maxEnemy);
            for (byte ii = 0; ii < enemysCount; ii++)
            {
                int enemyIndex = MyRandom.random.Next(0, enemys.Count);
                newMap.enemys[i][ii] = enemys[enemyIndex];
            }
        }

        mapData = newMap;
    }
}

public struct MapData
{
    public string mapModel;

    public byte wavesCount;
    public string[][] enemys;
}
