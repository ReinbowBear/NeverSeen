using System;
using UnityEngine;

public static class Random
{
    public static System.Random random;
    private static int seed;

    static Random()
    {
        seed = DateTime.Now.Millisecond;
        random = new System.Random(seed);

        EventBus.Add<MyEvent.OnSave>(Save);
        EventBus.Add<MyEvent.OnLoad>(Load);
    }


    private static void Save(MyEvent.OnSave _)
    {
        //SaveDangeonMap saveDangeonMap = new SaveDangeonMap();
        //saveDangeonMap.seed = seed;
//
        //SaveSystem.gameData.saveDangeonMap = saveDangeonMap;
    }

    private static void Load(MyEvent.OnLoad _)
    {
        //seed = SaveSystem.gameData.saveDangeonMap.seed;
        //random = new System.Random(seed);
    }
}

[System.Serializable]
public struct SaveRandom
{
    public int seed;
}
