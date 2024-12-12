using System;
using UnityEngine;

public class MapContent : MonoBehaviour
{
    [SerializeField] private GlobalMap globalMap;
    [SerializeField] private DangeonPanel mapPanel;

    private System.Random random;

    public void PrepareTiles()
    {
        //random = globalMap.random;
//
        //for (byte i = 0; i < globalMap.mapsNumber.Length; i++)
        //{
        //    for (byte ii = 0; ii < globalMap.pathCount; ii++)
        //    {
        //        for (byte iii = 0; iii < globalMap.mapHeight; iii++)
        //        {
        //            //Map map = globalMap.pathPoints[i, ii, iii];
        //            //map.index = new byte[] { i, ii, iii };
        //            //map.mapPanel = mapPanel;
        //            //map.mapData = GetMapData(iii);
        //        }
        //    }
        //}
    }

    private DangeonData GetMapData(byte mapHeight)
    {
        DangeonData mapData = new DangeonData();
//
        //mapData.mapIndex = random.Next(0, Content.data.maps.Length);
//
        //mapData.enemyIndex = new int[EnemyTypesCount[mapHeight]];
        //for (byte i = 0; i < EnemyTypesCount[mapHeight]; i++)
        //{
        //    mapData.enemyIndex[i] = random.Next(0, Content.data.enemys.Length);
        //}
//
        //mapData.enemyCount = new int[EnemyTypesCount[mapHeight]];
        //for (byte i = 0; i < EnemyTypesCount[mapHeight]; i++)
        //{
        //    mapData.enemyIndex[i] = random.Next(minEnemyCount, maxEnemyCount);
        //}
//
        return mapData;
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryMap>(PrepareTiles);
    }
}


public struct DangeonData
{
    public int mapIndex;

    public int[] enemyIndex;
    public int[] enemyCount;
}

